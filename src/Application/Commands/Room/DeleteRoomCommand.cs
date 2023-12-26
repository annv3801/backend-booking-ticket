using MediatR;

namespace Application.Commands.Room;

public class DeleteRoomCommand : IRequest<int>
{
    public long Id { get; set; }
}