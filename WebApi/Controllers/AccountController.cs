using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Commands.Account;
using Application.Common;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Account.Requests;
using Application.Queries;
using Application.Queries.Account;
using AutoMapper;
using Domain.Constants;
using Infrastructure.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
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
    [Route("Identity/Account")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateAccountAsync(CreateAccountRequest createAccountRequest, CancellationToken cancellationToken)
    {
        try
        {
            var createUserCommand = _mapper.Map<CreateAccountCommand>(createAccountRequest);
            var result = await _mediator.Send(createUserCommand, cancellationToken);
            if (result.Succeeded)
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
    [Route("Identity/Account/MyProfile")]
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
            if (result.Succeeded)
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
    [Route("Identity/Account/ChangePassword")]
    [Produces("application/json")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<ChangePasswordCommand>(changePasswordRequest);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Succeeded)
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
    [Route("Identity/Account/SignInWithPhoneNumber")]
    [Produces("application/json")]
    public async Task<IActionResult> SignInWithPhoneNumberAsync(SignInWithPhoneNumberRequest signInWithUserNameRequest, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<SignInWithPhoneNumberCommand>(signInWithUserNameRequest);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
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

        var claimsIdentity = new ClaimsIdentity(claims);
        return claimsIdentity;
    }
    private  string DecryptText(string cipherText, string encryptionPrivateKey = "") 
    {
        if (String.IsNullOrEmpty(cipherText))
            return cipherText;

    
        var tDESalg = new TripleDESCryptoServiceProvider();
        tDESalg.Key = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0, 16));
        tDESalg.IV = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(8, 8));

        byte[] buffer = Convert.FromBase64String(cipherText);
        return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
    }
    private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv) 
    {
        using (var ms = new MemoryStream(data)) {
            using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
            {
                using (var sr = new StreamReader(cs, Encoding.Unicode))
                {
                    return sr.ReadLine();
                }
            }
        }
    }

    [HttpDelete]
    // [Permissions]
    [Route("Identity/Account/Logout")]
    [Produces("application/json")]
    public async Task<IActionResult> LogoutAsync([FromQuery] bool forceEndOtherSessions, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new LogOutCommand()
            {
                ForceEndOtherSessions = forceEndOtherSessions
            }, cancellationToken);
            if (result.Succeeded)
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