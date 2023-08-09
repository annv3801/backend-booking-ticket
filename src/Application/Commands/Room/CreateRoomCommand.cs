using Application.DataTransferObjects.Room.Requests;
using MediatR;

namespace Application.Commands.Room;

public class CreateRoomCommand : CreateRoomRequest, IRequest<int>
{
    public CreateRoomCommand(Domain.Entities.Room entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Room Entity { get; set; }
}