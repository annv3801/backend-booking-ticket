using Application.DataTransferObjects.RoomSeat.Responses;
using MediatR;

namespace Application.Queries.RoomSeat;

public class GetRoomSeatByIdQuery : IRequest<RoomSeatResponse?>
{
    public long Id { get; set; }
}