using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Room.Responses;
using MediatR;

namespace Application.Queries.Room;
[ExcludeFromCodeCoverage]
public class ViewListRoomQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewRoomResponse>>>
{
}
