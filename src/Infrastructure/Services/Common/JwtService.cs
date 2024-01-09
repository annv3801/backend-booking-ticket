using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common;
using Application.Common.Configurations;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Jwt.Responses;
using Application.Interface;
using Domain.Constants;
using Domain.Entities.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nobi.Core.Responses;

namespace Infrastructure.Services.Common;
public class JwtService : IJwtService
{
    private readonly JwtConfiguration _jwtOptions;
    private readonly IDateTimeService _dateTime;

    public JwtService(IOptions<JwtConfiguration> jwtOptions, IDateTimeService dateTime)
    {
        _jwtOptions = jwtOptions.Value;
        _dateTime = dateTime;
    }

    public async Task<RequestResult<CreateJwtResponse>> GenerateJwtAsync(Account account, ClaimsIdentity claimsIdentity, CancellationToken cancellationToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SymmetricSecurityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddHours(_jwtOptions.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            await Task.CompletedTask;
            return RequestResult<CreateJwtResponse>.Succeed("Success", new CreateJwtResponse()
            {
                Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                RefreshToken = GenerateRefreshJwtToken(account, cancellationToken)
            });
        }
        catch (Exception e)
        {
            throw;
        }
    }

    // ReSharper disable once UnusedParameter.Local
    private string GenerateRefreshJwtToken(Account account, CancellationToken cancellationToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SymmetricSecurityKey);
            var claims = new List<Claim>();
            foreach (var userRole in account.AccountRoles)
            {
                if (userRole.Role == null) continue;
                claims.Add(new Claim(JwtClaimTypes.Role, userRole.Role.Name));

                claims.AddRange(from rolePermission in userRole.Role?.RolePermissions ?? new List<RolePermission>() where rolePermission.Permission != null select new Claim(JwtClaimTypes.Permission, rolePermission.Permission?.Code ?? string.Empty));
            }

            claims.Add(new Claim(JwtClaimTypes.IdentityProvider, Constants.LoginProviders.Self));
            claims.Add(new Claim(JwtClaimTypes.UserId, account.Id.ToString()));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_jwtOptions.RefreshTokenExpires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
