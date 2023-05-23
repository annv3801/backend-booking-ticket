using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Seat.Responses;
using MediatR;

namespace Application.Queries.Seat;
[ExcludeFromCodeCoverage]
public class ViewListSeatQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewSeatResponse>>>
{
}
