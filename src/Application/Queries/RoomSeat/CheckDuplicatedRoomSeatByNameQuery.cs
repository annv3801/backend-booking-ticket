using MediatR;

namespace Application.Queries.RoomSeat;

public class CheckDuplicatedRoomSeatByNameQuery :  IRequest<bool>
{
    public required string Name { get; set; }
}