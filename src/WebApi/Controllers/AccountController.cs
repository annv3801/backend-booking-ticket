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

    public AccountController(IMediator mediator, IMapper mapper, ICurrentAccountService currentAccountService, IAccountManagementService accountManagementService, ILoggerService loggerService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
        _accountManagementService = accountManagementService;
        _loggerService = loggerService;
    }
    
    [HttpPost]
    [Route("Create-Account")]
    [Produces("application/json")]

    public async Task<RequestResult<CreateAccountResponse>> CreateAccountAsync(CreateAccountRequest createAccountRequest, CancellationToken cancellationToken)
    {
        try
        {
            var createUserCommand = _mapper.Map<CreateAccountCommand>(createAccountRequest);
            var result = await _mediator.Send(createUserCommand, cancellationToken);
            if (result != null)
                return RequestResult<CreateAccountResponse>.Succeed("Save success");
            return RequestResult<CreateAccountResponse>.Fail("Save data fail");

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
            if (result != null)
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
            if (result != null)
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
            if (result != null)
                return RequestResult<SignInWithPhoneNumberResponse>.Succeed(null, result.Data);
            return RequestResult<SignInWithPhoneNumberResponse>.Fail(null, result.Errors);
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
            if (result != null)
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
}