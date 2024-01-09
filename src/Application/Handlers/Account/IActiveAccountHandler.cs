using Application.Commands.Account;
using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Handlers.Account;
public interface IActiveAccountHandler : IRequestHandler<ActiveAccountCommand, RequestResult<AccountResult>>
{
}
