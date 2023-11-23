using Application.Commands.Room;
using MediatR;

namespace Application.Handlers.Room;

public interface ICreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, int>
{
}

public interface IUpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, int>
{
}

public interface IDeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, int>
{
}
