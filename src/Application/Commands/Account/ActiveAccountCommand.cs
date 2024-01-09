using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.DataTransferObjects.Account.Responses;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class ActiveAccountCommand : IRequest<RequestResult<AccountResult>>
{
    public string Otp { get; set; }
}
