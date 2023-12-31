using System.Security.Claims;
using Application.Commands.Account;
using Application.Common;
using Application.Common.Configurations;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Account.Responses;
using Application.Interface;
using Application.Repositories.Account;
using Application.Services.Account;
using AutoMapper;
using Domain.Constants;
using Domain.Entities.Identity;
using Domain.Enums;
using Infrastructure.Databases;
using Infrastructure.Validators.Account;
using MediatR;
using Microsoft.Extensions.Options;
using Nobi.Core.Responses;

namespace Infrastructure.Services;
public class AccountManagementService : IAccountManagementService
{
    private readonly IDateTimeService _dateTime;
    private readonly IStringLocalizationService _localizationService;
    private readonly ApplicationConfiguration _appOption;
    private readonly IPasswordGeneratorService _passwordGeneratorService;
    private readonly IJwtService _jwtService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountTokenRepository _accountTokenRepository;
    private readonly IMediator _mediator;
    
    public AccountManagementService(IDateTimeService dateTime,
        IStringLocalizationService localizationService, 
        IOptions<ApplicationConfiguration> appOption, IPasswordGeneratorService passwordGeneratorService,
        IJwtService jwtService, IJsonSerializerService jsonSerializerService, IMapper mapper, ICurrentAccountService currentAccountService, ApplicationDbContext applicationDbContext, IAccountRepository accountRepository, IAccountTokenRepository accountTokenRepository, IMediator mediator)
    {
        _dateTime = dateTime;
        _localizationService = localizationService;
        _appOption = appOption.Value;
        _passwordGeneratorService = passwordGeneratorService;
        _jwtService = jwtService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
        _applicationDbContext = applicationDbContext;
        _accountRepository = accountRepository;
        _accountTokenRepository = accountTokenRepository;
        _mediator = mediator;
    }

    public async Task<RequestResult<AccountResult>> CreateAccountByAdminAsync(Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var validator = new CreateAccountEntityValidator(_localizationService, _accountRepository);
            var validation = await validator.ValidateAsync(account, cancellationToken);
            if (!validation.IsValid)
                return RequestResult<AccountResult>.Fail("Fail");
            var duplicatedEnumerable = await _accountRepository.IsDuplicatedUserNameAsync(account.Id, account.UserName, cancellationToken);
            if (duplicatedEnumerable)
                return RequestResult<AccountResult>.Fail("Duplicated Item");
            
            await _accountRepository.AddAsync(account, cancellationToken);
            var result = await _accountRepository.SaveChangesAsync(cancellationToken);
            if (result > 0)
                return RequestResult<AccountResult>.Succeed("Success", new AccountResult());
            return RequestResult<AccountResult>.Fail("Save Fail");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<RequestResult<AccountResult>> UpdateAccountAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check account
            var existedAccount = await _accountRepository.ViewAccountDetailByAdmin(account.Id, cancellationToken);

            if (existedAccount == null)
                return RequestResult<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value);

            //Check duplicated email 
            var emailCheck = await _accountRepository.IsDuplicatedEmailAsync(existedAccount.Id, existedAccount.Email, cancellationToken);
            if (emailCheck)
                return RequestResult<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedEmail].Value);

            //Check duplicated phone 
            var phoneCheck = await _accountRepository.IsDuplicatedPhoneNumberAsync(existedAccount.Id, account.PhoneNumber, cancellationToken);
            if (phoneCheck)
                return RequestResult<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedPhoneNumber].Value);

            // Check current account permission
            // If account is sysadmin --> can update
            // If account is tenant admin-> can update user who belong his tenant only --> return forbid http status code

            _mapper.Map(account, existedAccount);

            await _accountRepository.UpdateAsync(existedAccount, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
                return RequestResult<AccountResult>.Succeed();
            return RequestResult<AccountResult>.Fail("Save Fail");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<RequestResult<ViewAccountResponse>> ViewAccountDetailByAdminAsync(long accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var existedAccount = await _accountRepository.ViewAccountDetailByAdmin(accountId, cancellationToken);
            if (existedAccount == null)
            {
                return RequestResult<ViewAccountResponse>.Fail(_localizationService[LocalizationString.Account.NotFound].Value);
            }
            var accountDetailResponse = _mapper.Map<ViewAccountResponse>(existedAccount);
            return RequestResult<ViewAccountResponse>.Succeed("Success", accountDetailResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<RequestResult<AccountResult>> UnlockAccountAsync(long accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check account id
            var existedAccount = await _accountRepository.ViewMyAccountAsync(accountId, cancellationToken);
            if (existedAccount == null)
                return RequestResult<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value);

            // Check that account only can be unlocked if its current status is locked
            if (existedAccount.Status != AccountStatus.Locked)
                return RequestResult<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotLockedYet].Value);

            existedAccount.Status = AccountStatus.Active;

            await _accountRepository.UpdateAsync(existedAccount, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0) // Unlock Succeeded 
                return RequestResult<AccountResult>.Succeed();

            return RequestResult<AccountResult>.Fail("Save Fail");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<RequestResult<AccountResult>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var currentAccountId = _currentAccountService.Id;

            var currentAccount = await _accountRepository.ViewMyAccountAsync(currentAccountId, cancellationToken);

            // Check current password
            var isCurrentPassword = _passwordGeneratorService.VerifyHashPassword(currentAccount?.PasswordHash, request.CurrentPassword);
            if (!isCurrentPassword)
                return RequestResult<AccountResult>.Fail(_localizationService[LocalizationString.Account.PasswordIncorrect].Value);

            // Change password
            currentAccount.PasswordHash = _passwordGeneratorService.HashPassword(request.NewPassword);

            // Check ForceOtherSessionsLogout, force all other sessions must be logged out or not
            if (request.ForceOtherSessionsLogout)
            {
                // remove all jwt token to force user logout
                var listAccountTokens = await _accountTokenRepository.GetAccountTokensByAccountAsync(currentAccountId, cancellationToken);
                await _accountTokenRepository.RemoveRangeAsync(listAccountTokens, cancellationToken);

                // Delete user firebase token associated with current access token
            }

            if (!request.ForceOtherSessionsLogout)
            {
                // Get current token
                var token = _currentAccountService.GetJwtToken();
                var accountToken = await _accountTokenRepository.GetAccountTokensByTokenAsync(token, cancellationToken);
                // Get token firebase by token
                await _accountTokenRepository.RemoveAsync(accountToken, cancellationToken);
            }

            await _accountRepository.UpdateAsync(currentAccount, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
                return RequestResult<AccountResult>.Succeed();

            return RequestResult<AccountResult>.Fail(_localizationService[LocalizationString.Account.FailedToChangePassword].Value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<RequestResult<AccountResult>> UpdateMyAccountAsync(Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            //Get current account
            var currentAccountId = _currentAccountService.Id;

            // Check account 
            var existedAccount = await _accountRepository.ViewMyAccountAsync(currentAccountId, cancellationToken);
            if (existedAccount == null)
                return RequestResult<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value);

            //Check duplicated email only
            var emailCheck = await _accountRepository.IsDuplicatedEmailAsync(existedAccount.Id, account.Email, cancellationToken);
            if (emailCheck)
                return RequestResult<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedEmail].Value);

            //Check duplicated phone 
            var phoneCheck = await _accountRepository.IsDuplicatedPhoneNumberAsync(existedAccount.Id, account.PhoneNumber, cancellationToken);
            if (phoneCheck)
                return RequestResult<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedPhoneNumber].Value);

            existedAccount.Email = account.Email;
            existedAccount.NormalizedEmail = account.Email.ToUpper();
            existedAccount.PhoneNumber = account.PhoneNumber;
            existedAccount.FullName = account.FullName;
            existedAccount.Gender = account.Gender;
            existedAccount.AvatarPhoto = account.AvatarPhoto;
            
            await _accountRepository.UpdateAsync(existedAccount, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
                return RequestResult<AccountResult>.Succeed();
            return RequestResult<AccountResult>.Fail("Save Fail");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<RequestResult<SignInWithPhoneNumberResponse>> SignInWithPhoneNumberAsync(SignInWithPhoneNumberRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find the user with status is not inactive
            var account = await _accountRepository.GetAccountByPhoneNumberAsync(request.PhoneNumber, cancellationToken);
            if (account == null)
                return RequestResult<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.NotFound].Value);
            // If user is not active, return error
            switch (account.Status)
            {
                case AccountStatus.Inactive:
                    return RequestResult<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsNotActive].Value);
                case AccountStatus.PendingApproval:
                    return RequestResult<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsPendingApproval].Value);
                // case AccountStatus.Locked:
                //     return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsLocked].Value);
                case AccountStatus.PendingConfirmation:
                    return RequestResult<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsPendingConfirmation].Value);
            }

            // Verify password or temporary password, if both of them are wrong, return
            if (!_passwordGeneratorService.VerifyHashPassword(account.PasswordHash, request.Password) &&
                !_passwordGeneratorService.VerifyHashPassword(account.PasswordHashTemporary, request.Password))
            {
                // If user lockout enabled, check them and process
                if (account.LockoutEnabled)
                {
                    account.AccessFailedCount += 1;
                    if (account.AccessFailedCount > _appOption.LockoutLimit - 1)
                    {
                        account.LockoutEnd = DateTime.UtcNow.AddMinutes(_appOption.LockoutDurationInMin);
                        account.Status = AccountStatus.Locked;
                        await _accountRepository.UpdateAsync(account, cancellationToken);
                        await _applicationDbContext.SaveChangesAsync(cancellationToken);
                        return RequestResult<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.AccountLockedOut]);
                    }

                    await _accountRepository.UpdateAsync(account, cancellationToken);
                    await _applicationDbContext.SaveChangesAsync(cancellationToken);
                    return RequestResult<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.UserNameOrPasswordIncorrectWithLockoutEnabled, _appOption.LockoutLimit - account.AccessFailedCount]);
                }

                return RequestResult<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.UserNameOrPasswordIncorrectWithoutLockOut]);
            }

            // Check lockout time, return error if we're still in the locked period
            if (account.LockoutEnd != null && account.LockoutEnd > DateTime.UtcNow)
                return RequestResult<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.LockedOutAffected]);
            // Check password expiration 
            if (account.PasswordValidUntilDate != null && account.PasswordValidUntilDate < DateTime.UtcNow)
                return RequestResult<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.PasswordExpired]);
            // If we need to change password at first log in, force them to change
            if (account.PasswordChangeRequired)
                return RequestResult<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.ChangePasswordRequired]);

            // Get roles + permissions

            var claimsIdentity = BuildClaimsIdentity(account);
            var tokenResponse = await _jwtService.GenerateJwtAsync(account, claimsIdentity, cancellationToken);
            account.AccountTokens = new List<AccountToken>()
            {
                new AccountToken()
                {
                    AccountId = account.Id,
                    Name = Guid.NewGuid().ToString().ToUpper(),
                    LoginProvider = Constants.LoginProviders.Self,
                    Token = tokenResponse.Data.Token,
                    RefreshToken = tokenResponse.Data.RefreshToken
                }
            };
            // Reset failed count and locked out date
            account.AccessFailedCount = 0;
            account.LockoutEnd = null;
            await _accountRepository.UpdateAsync(account, cancellationToken);

            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                return RequestResult<SignInWithPhoneNumberResponse>.Succeed("Success", new SignInWithPhoneNumberResponse()
                {
                    Profile = new ProfileAccount()
                    {
                        Email = account.Email,
                        PhoneNumber = account.PhoneNumber,
                        Gender = account.Gender,
                        Avatar = account.AvatarPhoto,
                        FullName = account.FullName,
                    },
                    AccessToken = tokenResponse.Data.Token,
                    RefreshToken = tokenResponse.Data.RefreshToken
                });
            }
            return RequestResult<SignInWithPhoneNumberResponse>.Fail("Save Fail");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<RequestResult<AccountResult>> LogOutAsync(bool forceEndOtherSessions = false, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var currentAccountId = _currentAccountService.Id;
            var account = await _accountRepository.ViewMyAccountAsync(currentAccountId, cancellationToken);
            if (account == null)
                return RequestResult<AccountResult>.Fail("Account is not found, deactivated or deleted");
            if (forceEndOtherSessions)
            {
                // remove all jwt token to force user logout
                var listAccountTokens = await _accountTokenRepository.GetAccountTokensByAccountAsync(currentAccountId, cancellationToken);
                await _accountTokenRepository.RemoveRangeAsync(listAccountTokens, cancellationToken);
            }

            if (!forceEndOtherSessions)
            {
                // Get current token
                var token = _currentAccountService.GetJwtToken();
                var accountToken = await _accountTokenRepository.GetAccountTokensByTokenAsync(token, cancellationToken);
                // Get token firebase by token
                await _accountTokenRepository.RemoveAsync(accountToken, cancellationToken);
            }

            await _accountRepository.UpdateAsync(account, cancellationToken);

            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
                return RequestResult<AccountResult>.Succeed();
            return RequestResult<AccountResult>.Fail("Save fail");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<RequestResult<bool>> CreateAndUpdateAccountCategoryAsync(CreateAndUpdateAccountCategoryRequest request, CancellationToken cancellationToken)
    {
        var resultUpdateCategory = await _mediator.Send(new CreateAndUpdateAccountCategoryCommand()
        {
            AccountId = _currentAccountService.Id,
            CategoryIds = request.CategoryIds
        }, cancellationToken);
        if (resultUpdateCategory <= 0)
            return RequestResult<bool>.Fail("Save data failed");
        return RequestResult<bool>.Succeed("Save data success");
    }

    public async Task<RequestResult<bool>> CreateAndUpdateAccountFavoriteAsync(CreateAndUpdateAccountFavoriteRequest request, CancellationToken cancellationToken)
    {
        var resultUpdateCategory = await _mediator.Send(new CreateAndUpdateAccountFavoriteCommand()
        {
            AccountId = _currentAccountService.Id,
            FilmId = request.FilmId,
            TheaterId = request.TheaterId
        }, cancellationToken);
        if (resultUpdateCategory <= 0)
            return RequestResult<bool>.Fail("Save data failed");
        return RequestResult<bool>.Succeed("Save data success");
    }

    private static ClaimsIdentity BuildClaimsIdentity(Domain.Entities.Identity.Account account)
    {
        var claims = new List<Claim>();

        claims.Add(new Claim(JwtClaimTypes.IdentityProvider, Constants.LoginProviders.Self));
        claims.Add(new Claim(JwtClaimTypes.UserId, account.Id.ToString()));
        claims.Add(new Claim(JwtClaimTypes.UserName, account.UserName));

        var claimsIdentity = new ClaimsIdentity(claims);
        return claimsIdentity;
    }
}
