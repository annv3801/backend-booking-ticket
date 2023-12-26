using Domain.Entities;
using MediatR;

namespace Application.Commands.Room;

public class UpdateRoomCommand : IRequest<int>
{
    public RoomEntity Request { get; set; }
}