using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class UpdateAccountCommand : IRequest<RequestResult<AccountResult>>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string? FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string? AvatarPhoto { get; set; }
    public bool? Gender { get; set; }
}
