using System.Security.Claims;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models.Account;
using Domain.Constants;
using Domain.Exceptions;
using IdentityModel;
using JwtClaimTypes = Domain.Constants.JwtClaimTypes;

namespace WebApi.Services;

public class CurrentAccountService : ICurrentAccountService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentAccountService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid Id
    {
        get
        {
            Guid.TryParse(_httpContextAccessor.HttpContext?.User.FindFirstValue(JwtClaimTypes.UserId),
                out var userId);
            if (userId == Guid.Empty)
            {
                throw new AccountNotFoundException("You are not logged in yet");
            }

            return userId;
        }
    }

    public GeneralAccountView Account
    {
        get { return new GeneralAccountView(); }
    }

    public string GetJwtToken()
    {
        var result =
            _httpContextAccessor.HttpContext?.Request.Headers.FirstOrDefault(h => h.Key == "Authorization");
        return result?.Value.ToString().Replace("Bearer ", "");
    }
}
