using Application.Commands.Account;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Handlers.Account;
public interface ISignInWithPhoneNumberHandler : IRequestHandler<SignInWithPhoneNumberCommand, RequestResult<SignInWithPhoneNumberResponse>>
{
    
}
