using System.Security.Claims;
using Application.Common;
using Application.Common.Configurations;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Account.Responses;
using Application.Logging.ActionLog.Services;
using Application.Repositories.Account;
using Application.Services.Account;
using AutoMapper;
using Domain.Constants;
using Domain.Entities.Identity;
using Domain.Enums;
using Infrastructure.Databases;
using Infrastructure.Validators.Account;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;
public class AccountManagementService : IAccountManagementService
{
    private readonly IDateTime _dateTime;
    private readonly IStringLocalizationService _localizationService;
    private readonly IActionLogService _actionLogService;
    private readonly ApplicationConfiguration _appOption;
    private readonly IPasswordGeneratorService _passwordGeneratorService;
    private readonly IJwtService _jwtService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IPaginationService _paginationService;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountTokenRepository _accountTokenRepository;
    
    public AccountManagementService(IDateTime dateTime,
        IStringLocalizationService localizationService, IActionLogService actionLogService,
        IOptions<ApplicationConfiguration> appOption, IPasswordGeneratorService passwordGeneratorService,
        IJwtService jwtService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService, ICurrentAccountService currentAccountService, ApplicationDbContext applicationDbContext, IAccountRepository accountRepository, IAccountTokenRepository accountTokenRepository)
    {
        _dateTime = dateTime;
        _localizationService = localizationService;
        _actionLogService = actionLogService;
        _appOption = appOption.Value;
        _passwordGeneratorService = passwordGeneratorService;
        _jwtService = jwtService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
        _currentAccountService = currentAccountService;
        _applicationDbContext = applicationDbContext;
        _accountRepository = accountRepository;
        _accountTokenRepository = accountTokenRepository;
    }

    public async Task<Result<AccountResult>> CreateAccountByAdminAsync(Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var validator = new CreateAccountEntityValidator(_localizationService, _accountRepository);
            var validation = await validator.ValidateAsync(account, cancellationToken);
            if (!validation.IsValid)
                return Result<AccountResult>.Fail(validation.Errors.BuildArray());
            var duplicatedEnumerable = await _accountRepository.IsDuplicatedUserNameAsync(account.Id, account.UserName, cancellationToken);
            if (duplicatedEnumerable)
                return Result<AccountResult>.Fail(Constants.DuplicatedItem);
            
            await _accountRepository.AddAsync(account, cancellationToken);
            var result = await _accountRepository.SaveChangesAsync(cancellationToken);
            if (result > 0)
                return Result<AccountResult>.Succeed(new AccountResult());
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<AccountResult>> UpdateAccountAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check account
            var existedAccount = await _accountRepository.ViewAccountDetailByAdmin(account.Id, cancellationToken);

            if (existedAccount == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));

            //Check duplicated email 
            var emailCheck = await _accountRepository.IsDuplicatedEmailAsync(existedAccount.Id, existedAccount.Email, cancellationToken);
            if (emailCheck)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedEmail].Value.ToErrors(_localizationService));

            //Check duplicated phone 
            var phoneCheck = await _accountRepository.IsDuplicatedPhoneNumberAsync(existedAccount.Id, account.PhoneNumber, cancellationToken);
            if (phoneCheck)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedPhoneNumber].Value.ToErrors(_localizationService));

            // Check current account permission
            // If account is sysadmin --> can update
            // If account is tenant admin-> can update user who belong his tenant only --> return forbid http status code

            _mapper.Map(account, existedAccount);

            await _accountRepository.UpdateAsync(existedAccount, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
                return Result<AccountResult>.Succeed();
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewAccountResponse>> ViewAccountDetailByAdminAsync(long accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var existedAccount = await _accountRepository.ViewAccountDetailByAdmin(accountId, cancellationToken);
            if (existedAccount == null)
            {
                return Result<ViewAccountResponse>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));
            }
            var accountDetailResponse = _mapper.Map<ViewAccountResponse>(existedAccount);
            return Result<ViewAccountResponse>.Succeed(accountDetailResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<AccountResult>> UnlockAccountAsync(long accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check account id
            var existedAccount = await _accountRepository.ViewMyAccountAsync(accountId, cancellationToken);
            if (existedAccount == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));

            // Check that account only can be unlocked if its current status is locked
            if (existedAccount.Status != AccountStatus.Locked)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotLockedYet].Value.ToErrors(_localizationService));

            existedAccount.Status = AccountStatus.Active;

            await _accountRepository.UpdateAsync(existedAccount, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0) // Unlock Succeeded 
                return Result<AccountResult>.Succeed();

            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<AccountResult>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var currentAccountId = _currentAccountService.Id;

            var currentAccount = await _accountRepository.ViewMyAccountAsync(currentAccountId, cancellationToken);

            // Check current password
            var isCurrentPassword = _passwordGeneratorService.VerifyHashPassword(currentAccount?.PasswordHash, request.CurrentPassword);
            if (!isCurrentPassword)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.PasswordIncorrect].Value.ToErrors(_localizationService));

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
                return Result<AccountResult>.Succeed();

            return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.FailedToChangePassword].Value.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<AccountResult>> UpdateMyAccountAsync(Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            //Get current account
            var currentAccountId = _currentAccountService.Id;

            // Check account 
            var existedAccount = await _accountRepository.ViewMyAccountAsync(currentAccountId, cancellationToken);
            if (existedAccount == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));

            //Check duplicated email only
            var emailCheck = await _accountRepository.IsDuplicatedEmailAsync(existedAccount.Id, account.Email, cancellationToken);
            if (emailCheck)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedEmail].Value.ToErrors(_localizationService));

            //Check duplicated phone 
            var phoneCheck = await _accountRepository.IsDuplicatedPhoneNumberAsync(existedAccount.Id, account.PhoneNumber, cancellationToken);
            if (phoneCheck)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedPhoneNumber].Value.ToErrors(_localizationService));

            //Using automapper
            _mapper.Map(account, existedAccount);

            await _accountRepository.UpdateAsync(existedAccount, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
                return Result<AccountResult>.Succeed();
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<SignInWithPhoneNumberResponse>> SignInWithPhoneNumberAsync(SignInWithPhoneNumberRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find the user with status is not inactive
            var account = await _accountRepository.GetAccountByPhoneNumberAsync(request.PhoneNumber, cancellationToken);
            if (account == null)
                return Result<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));
            // If user is not active, return error
            switch (account.Status)
            {
                case AccountStatus.Inactive:
                    return Result<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsNotActive].Value.ToErrors(_localizationService));
                case AccountStatus.PendingApproval:
                    return Result<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsPendingApproval].Value.ToErrors(_localizationService));
                // case AccountStatus.Locked:
                //     return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsLocked].Value.ToErrors(_localizationService));
                case AccountStatus.PendingConfirmation:
                    return Result<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsPendingConfirmation].Value.ToErrors(_localizationService));
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
                        account.LockoutEnd = _dateTime.UtcNow.AddMinutes(_appOption.LockoutDurationInMin);
                        account.Status = AccountStatus.Locked;
                        await _accountRepository.UpdateAsync(account, cancellationToken);
                        await _applicationDbContext.SaveChangesAsync(cancellationToken);
                        return Result<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.AccountLockedOut].Value.ToErrors(_localizationService));
                    }

                    await _accountRepository.UpdateAsync(account, cancellationToken);
                    await _applicationDbContext.SaveChangesAsync(cancellationToken);
                    return Result<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.UserNameOrPasswordIncorrectWithLockoutEnabled, _appOption.LockoutLimit - account.AccessFailedCount].Value.ToErrors(_localizationService));
                }

                return Result<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.UserNameOrPasswordIncorrectWithoutLockOut].Value.ToErrors(_localizationService));
            }

            // Check lockout time, return error if we're still in the locked period
            if (account.LockoutEnd != null && account.LockoutEnd > _dateTime.UtcNow)
                return Result<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.LockedOutAffected, account.LockoutEnd.ToFormattedString()].Value.ToErrors(_localizationService));
            // Check password expiration 
            if (account.PasswordValidUntilDate != null && account.PasswordValidUntilDate < _dateTime.UtcNow)
                return Result<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.PasswordExpired].Value.ToErrors(_localizationService));
            // If we need to change password at first log in, force them to change
            if (account.PasswordChangeRequired)
                return Result<SignInWithPhoneNumberResponse>.Fail(_localizationService[LocalizationString.Account.ChangePasswordRequired].Value.ToErrors(_localizationService));

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
                return Result<SignInWithPhoneNumberResponse>.Succeed(new SignInWithPhoneNumberResponse()
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
            return Result<SignInWithPhoneNumberResponse>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<AccountResult>> LogOutAsync(bool forceEndOtherSessions = false, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var currentAccountId = _currentAccountService.Id;
            var account = await _accountRepository.ViewMyAccountAsync(currentAccountId, cancellationToken);
            if (account == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));
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
                return Result<AccountResult>.Succeed();
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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
