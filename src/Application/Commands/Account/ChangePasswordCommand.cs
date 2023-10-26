using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class ChangePasswordCommand : IRequest<RequestResult<AccountResult>>
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
    public string ForceOtherSessionsLogout { get; set; }
    
}
