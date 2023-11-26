using Domain.Entities;
using MediatR;

namespace Application.Commands.RoomSeat;

public class UpdateRoomSeatCommand : IRequest<int>
{
    public required RoomSeatEntity Request { get; set; }
}