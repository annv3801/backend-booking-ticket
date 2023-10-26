using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class LogOutCommand : IRequest<RequestResult<AccountResult>>
{
    public bool ForceEndOtherSessions { get; set; }
}
