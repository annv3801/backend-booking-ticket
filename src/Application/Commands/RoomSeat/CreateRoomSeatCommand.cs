using Domain.Entities;
using MediatR;

namespace Application.Commands.RoomSeat;

public class CreateRoomSeatCommand : IRequest<int>
{
    public List<RoomSeatEntity> Entity { get; set; }
}