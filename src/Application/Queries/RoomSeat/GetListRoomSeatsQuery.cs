using Application.DataTransferObjects.RoomSeat.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.RoomSeat;

public class GetListRoomSeatsQuery : IRequest<OffsetPaginationResponse<RoomSeatResponse>>
{
    public OffsetPaginationRequest OffsetPaginationRequest { get; set; }
}