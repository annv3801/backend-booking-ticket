using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Queries.Account;
[ExcludeFromCodeCoverage]
public class ViewAccountQuery: IRequest<RequestResult<ViewAccountResponse>>
{
    // Should be used in the controller as input parameters
    public long AccountId { get; set; }
}
