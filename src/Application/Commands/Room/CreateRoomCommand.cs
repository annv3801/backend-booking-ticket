using Domain.Entities;
using MediatR;

namespace Application.Commands.Room;

public class CreateRoomCommand : IRequest<int>
{
    public required RoomEntity Entity { get; set; }
}