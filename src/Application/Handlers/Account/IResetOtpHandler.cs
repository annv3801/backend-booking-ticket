using Application.Commands.Account;
using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Handlers.Account;
public interface IResetOtpHandler : IRequestHandler<ResetOtpCommand, RequestResult<AccountResult>>
{
}
