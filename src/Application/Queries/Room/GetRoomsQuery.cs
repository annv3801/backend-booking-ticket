using Application.DataTransferObjects.Room.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Room;

public class GetListRoomsQuery : IRequest<OffsetPaginationResponse<RoomResponse>>
{
    public required OffsetPaginationRequest OffsetPaginationRequest { get; set; }
}