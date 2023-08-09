using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using MediatR;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class UnlockAccountCommand : IRequest<Result<AccountResult>>
{
    public Guid Id { get; set; }
}
