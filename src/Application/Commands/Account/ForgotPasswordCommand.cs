using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class ForgotPasswordCommand: IRequest<RequestResult<AccountResult>>
{
    public string PhoneNumber { get; set; }
    public string? CaptchaToken { get; set; }

}
