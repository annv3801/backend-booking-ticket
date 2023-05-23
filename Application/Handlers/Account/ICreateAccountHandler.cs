using Application.Commands.Account;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;

namespace Application.Handlers.Account;
public interface ICreateAccountHandler : IRequestHandler<CreateAccountCommand, Result<CreateAccountResponse>>
{
}
