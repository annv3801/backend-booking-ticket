using Application.Commands.Room;
using MediatR;

namespace Application.Handlers.Room;

public interface IDeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, int>
{
}