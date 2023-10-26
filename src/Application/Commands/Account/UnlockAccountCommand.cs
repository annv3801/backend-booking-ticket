using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class UnlockAccountCommand : IRequest<RequestResult<AccountResult>>
{
    public Guid Id { get; set; }
}
