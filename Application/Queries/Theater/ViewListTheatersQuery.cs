using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Theater.Responses;
using MediatR;

namespace Application.Queries.Theater;
[ExcludeFromCodeCoverage]
public class ViewListTheatersQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewTheaterResponse>>>
{
}
