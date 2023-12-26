using MediatR;

namespace Application.Commands.RoomSeat;

public class DeleteRoomSeatCommand : IRequest<int>
{
    public long Id { get; set; }
}