using MediatR;

namespace Application.Queries.RoomSeat;

public class CheckDuplicatedRoomSeatByNameQuery :  IRequest<bool>
{
    public string Name { get; set; }
    public long RoomId { get; set; }
}