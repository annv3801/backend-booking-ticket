using Application.Commands.Account;
using Application.Common;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Account.Responses;
using Application.Queries.Account;
using Application.Services.Account;
using AutoMapper;
using Domain.Common.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;
[ApiController]
[Route("Account")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IAccountManagementService _accountManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IFileService _fileService;

    public AccountController(IMediator mediator, IMapper mapper, ICurrentAccountService currentAccountService, IAccountManagementService accountManagementService, ILoggerService loggerService, IFileService fileService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
        _accountManagementService = accountManagementService;
        _loggerService = loggerService;
        _fileService = fileService;
    }
    
    [HttpPost]
    [Route("Create-Account")]
    [Produces("application/json")]

    public async Task<RequestResult<CreateAccountResponse>> CreateAccountAsync([FromForm]CreateAccountRequest createAccountRequest, CancellationToken cancellationToken)
    {
        try
        {
            var image = "";
            if (createAccountRequest.AvatarPhoto != null)
            {
                var fileResult = _fileService.SaveImage(createAccountRequest.AvatarPhoto);
                if (fileResult.Item1 == 1)
                {
                    image = fileResult.Item2; // getting name of image
                }
            }

            var createAccountCommand = new CreateAccountCommand()
            {
                AvatarPhoto = image,
                Email = createAccountRequest.Email,
                Gender = createAccountRequest.Gender,
                Password = createAccountRequest.Password,
                FullName = createAccountRequest.FullName,
                PhoneNumber = createAccountRequest.PhoneNumber,
                UserName = createAccountRequest.UserName,
            };
            var result = await _mediator.Send(createAccountCommand, cancellationToken);
            if (result.Success)
                return RequestResult<CreateAccountResponse>.Succeed("Save success");
            return RequestResult<CreateAccountResponse>.Fail(result.Message);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut]
    [Route("Update-Account")]
    [Produces("application/json")]
    public async Task<RequestResult<CreateAccountResponse>> UpdateAccountAsync([FromForm]UpdateAccountRequest createAccountRequest, CancellationToken cancellationToken)
    {
        try
        {
            var currentUserId = _currentAccountService.Id;
            var image = "";
            if (createAccountRequest.AvatarPhoto != null)
            {
                var fileResult = _fileService.SaveImage(createAccountRequest.AvatarPhoto);
                if (fileResult.Item1 == 1)
                {
                    image = fileResult.Item2; // getting name of image
                }
            }

            var createAccountCommand = new UpdateAccountCommand()
            {
                Id = currentUserId,
                AvatarPhoto = image,
                Email = createAccountRequest.Email,
                Gender = createAccountRequest.Gender,
                FullName = createAccountRequest.FullName,
                PhoneNumber = createAccountRequest.PhoneNumber,
            };
            var result = await _mediator.Send(createAccountCommand, cancellationToken);
            if (result.Success)
                return RequestResult<CreateAccountResponse>.Succeed("Save success");
            return RequestResult<CreateAccountResponse>.Fail(result.Message);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    [Route("View-My-Account")]
    [Produces("application/json")]

    public async Task<RequestResult<ViewAccountResponse>> ViewMyAccountAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var currentUserId = _currentAccountService.Id;
            var result = await _mediator.Send(new ViewAccountQuery()
            {
                AccountId = currentUserId
            }, cancellationToken);
            if (result.Success)
                return RequestResult<ViewAccountResponse>.Succeed(null, result.Data);
            return RequestResult<ViewAccountResponse>.Fail(null, result.Errors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpPut]
    [Route("Change-Password")]
    [Produces("application/json")]
    public async Task<RequestResult<AccountResult>> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<ChangePasswordCommand>(changePasswordRequest);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Success)
                return RequestResult<AccountResult>.Succeed("Save data success");
            return RequestResult<AccountResult>.Fail(null, result.Errors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    [Route("Sign-In")]
    [Produces("application/json")]
    public async Task<RequestResult<SignInWithPhoneNumberResponse>> SignInWithPhoneNumberAsync(SignInWithPhoneNumberRequest signInWithUserNameRequest, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<SignInWithPhoneNumberCommand>(signInWithUserNameRequest);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Success)
                return RequestResult<SignInWithPhoneNumberResponse>.Succeed(null, result.Data);
            return RequestResult<SignInWithPhoneNumberResponse>.Fail(result.Message, result.Errors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpDelete]
    // [Permissions]
    [Route("Logout")]
    [Produces("application/json")]
    public async Task<RequestResult<AccountResult>> LogoutAsync([FromQuery] bool forceEndOtherSessions, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new LogOutCommand()
            {
                ForceEndOtherSessions = forceEndOtherSessions
            }, cancellationToken);
            if (result.Success)
                return RequestResult<AccountResult>.Succeed("Log out success");
            return RequestResult<AccountResult>.Fail("Log out fail");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPost]
    [Route("Create-Or-Update-Account-Category")]
    public async Task<RequestResult<bool>?> CreateOrUpdateAccountCategoryAsync(CreateAndUpdateAccountCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _accountManagementService.CreateAndUpdateAccountCategoryAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateOrUpdateAccountCategoryAsync));
            throw;
        }
    }
    
    [HttpPost]
    [Route("Create-Or-Update-Account-Favorite")]
    public async Task<RequestResult<bool>?> CreateOrUpdateAccountFavoriteAsync(CreateAndUpdateAccountFavoriteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _accountManagementService.CreateAndUpdateAccountFavoriteAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateOrUpdateAccountCategoryAsync));
            throw;
        }
    }
}