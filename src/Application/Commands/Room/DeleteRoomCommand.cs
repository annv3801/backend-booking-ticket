using MediatR;

namespace Application.Commands.Room;

public class DeleteRoomCommand : IRequest<int>
{
    public required long Id { get; set; }
}