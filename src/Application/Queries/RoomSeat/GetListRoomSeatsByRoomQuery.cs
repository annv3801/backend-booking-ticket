using Application.DataTransferObjects.RoomSeat.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.RoomSeat;

public class GetListRoomSeatsByRoomQuery : IRequest<ICollection<RoomSeatResponse>>
{
    public long RoomId { get; set; }
}