using Application.Commands.Account;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Account.Requests;
using Application.Queries.Account;
using AutoMapper;
using Infrastructure.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("Account")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;

    public AccountController(IMediator mediator, IMapper mapper, ICurrentAccountService currentAccountService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
    }
    
    [HttpPost]
    [Route("Create-Account")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateAccountAsync(CreateAccountRequest createAccountRequest, CancellationToken cancellationToken)
    {
        try
        {
            var createUserCommand = _mapper.Map<CreateAccountCommand>(createAccountRequest);
            var result = await _mediator.Send(createUserCommand, cancellationToken);
            if (result != null)
                return Ok(new SuccessResponse(data: new {result.Data}));
            return Accepted(new FailureResponse(result.Errors));
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

    public async Task<IActionResult> ViewMyAccountAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var currentUserId = _currentAccountService.Id;
            var result = await _mediator.Send(new ViewAccountQuery()
            {
                AccountId = currentUserId
            }, cancellationToken);
            if (result != null)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
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
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<ChangePasswordCommand>(changePasswordRequest);
            var result = await _mediator.Send(command, cancellationToken);
            if (result != null)
                return Ok(new SuccessResponse());
            return Accepted(new FailureResponse(result.Errors));
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
    public async Task<IActionResult> SignInWithPhoneNumberAsync(SignInWithPhoneNumberRequest signInWithUserNameRequest, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<SignInWithPhoneNumberCommand>(signInWithUserNameRequest);
            var result = await _mediator.Send(command, cancellationToken);
            if (result != null)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
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
    public async Task<IActionResult> LogoutAsync([FromQuery] bool forceEndOtherSessions, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new LogOutCommand()
            {
                ForceEndOtherSessions = forceEndOtherSessions
            }, cancellationToken);
            if (result != null)
                return Ok(new SuccessResponse());
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}