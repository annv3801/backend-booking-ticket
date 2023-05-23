using Application.Commands.Room;
using MediatR;

namespace Application.Handlers.Room;

public interface ICreateRoomCommandHandler: IRequestHandler<CreateRoomCommand, int>
{
    
}