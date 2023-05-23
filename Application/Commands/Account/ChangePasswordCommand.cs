using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using MediatR;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class ChangePasswordCommand : IRequest<Result<AccountResult>>
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
    public string ForceOtherSessionsLogout { get; set; }
    
}
