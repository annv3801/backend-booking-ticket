using Domain.Entities;
using MediatR;

namespace Application.Commands.Room;

public class CreateRoomCommand : IRequest<int>
{
    public RoomEntity Entity { get; set; }
}