using Domain.Entities;
using MediatR;

namespace Application.Commands.RoomSeat;

public class CreateRoomSeatCommand : IRequest<int>
{
    public RoomSeatEntity Entity { get; set; }
}