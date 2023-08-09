using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using Application.Queries;
using Application.Queries.Account;
using MediatR;

namespace Application.Handlers.Account;
public interface IViewAccountHandler  : IRequestHandler<ViewAccountQuery,Result<ViewAccountResponse>>
{
    
}
