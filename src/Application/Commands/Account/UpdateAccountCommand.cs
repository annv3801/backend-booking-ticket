using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using MediatR;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class UpdateAccountCommand : IRequest<Result<AccountResult>>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? AvatarPhoto { get; set; }
    public string? CoverPhoto { get; set; }
    public bool? Gender { get; set; }
}
