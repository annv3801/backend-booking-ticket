using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;

namespace Application.Queries.Account;
[ExcludeFromCodeCoverage]
public class ViewAccountQuery: IRequest<Result<ViewAccountResponse>>
{
    // Should be used in the controller as input parameters
    public Guid AccountId { get; set; }
}
