using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using MediatR;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class LogOutCommand : IRequest<Result<AccountResult>>
{
    public bool ForceEndOtherSessions { get; set; }
}
