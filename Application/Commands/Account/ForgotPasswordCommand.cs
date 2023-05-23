using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using MediatR;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class ForgotPasswordCommand: IRequest<Result<AccountResult>>
{
    public string PhoneNumber { get; set; }
    public string? CaptchaToken { get; set; }

}
