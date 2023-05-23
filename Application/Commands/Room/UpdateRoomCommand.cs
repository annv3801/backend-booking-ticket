using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Room.Requests;
using MediatR;

namespace Application.Commands.Room;
[ExcludeFromCodeCoverage]
public class UpdateRoomCommand : UpdateRoomRequest, IRequest<int>
{
    public UpdateRoomCommand(Domain.Entities.Room entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Room Entity { get; set; }
}
