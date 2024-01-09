using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.DataTransferObjects.Account.Responses;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class ResetOtpCommand : IRequest<RequestResult<AccountResult>>
{
}
