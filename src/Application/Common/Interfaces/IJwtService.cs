using System.Security.Claims;
using Application.Common.Models;
using Application.DataTransferObjects.Jwt.Responses;
using Domain.Entities.Identity;
using Nobi.Core.Responses;

namespace Application.Common.Interfaces;
public interface IJwtService
{
    Task<RequestResult<CreateJwtResponse>> GenerateJwtAsync(Account account, ClaimsIdentity claimsIdentity, CancellationToken cancellationToken);
}