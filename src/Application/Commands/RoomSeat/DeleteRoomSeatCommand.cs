using MediatR;

namespace Application.Commands.RoomSeat;

public class DeleteRoomSeatCommand : IRequest<int>
{
    public required long Id { get; set; }
}