using Application.Commands.Account;
using Application.Common;
using Application.Common.Models;
using MediatR;

namespace Application.Handlers.Account;
public interface IForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, Result<AccountResult>>
{
}
