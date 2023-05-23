using System.Security.Claims;
using Application.Common.Models;
using Application.DataTransferObjects.Jwt.Responses;
using Domain.Entities.Identity;

namespace Application.Common.Interfaces;
public interface IJwtService
{
    Task<Result<CreateJwtResponse>> GenerateJwtAsync(Account account, ClaimsIdentity claimsIdentity, CancellationToken cancellationToken);
}