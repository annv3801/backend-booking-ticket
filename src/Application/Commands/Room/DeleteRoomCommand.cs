using MediatR;

namespace Application.Commands.Room;

public class DeleteRoomCommand : IRequest<int>
{
    public DeleteRoomCommand(Domain.Entities.Room entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Room Entity { get; set; }
}