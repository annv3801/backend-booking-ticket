using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Account.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Queries.Account;
[ExcludeFromCodeCoverage]
public class ViewListAccountsQuery : OffsetPaginationRequest, IRequest<RequestResult<OffsetPaginationResponse<ViewAccountResponse>>>
{
}
