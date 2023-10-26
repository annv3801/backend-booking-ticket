using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class CreateAccountCommand : IRequest<Result<CreateAccountResponse>>
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AvatarPhoto { get; set; }
    public bool? Gender { get; set; }
    public string Password { get; set; }
}
