using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using MediatR;

namespace Application.Queries.Account;
[ExcludeFromCodeCoverage]
public class ViewListAccountsQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewAccountResponse>>>
{
}
