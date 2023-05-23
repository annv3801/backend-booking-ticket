using Application.Commands.Room;
using MediatR;

namespace Application.Handlers.Room;

public interface IUpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, int>
{
}