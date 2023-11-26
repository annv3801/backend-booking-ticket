using Application.Commands.RoomSeat;
using MediatR;

namespace Application.Handlers.RoomSeat;

public interface ICreateRoomSeatCommandHandler : IRequestHandler<CreateRoomSeatCommand, int>
{
}

public interface IUpdateRoomSeatCommandHandler : IRequestHandler<UpdateRoomSeatCommand, int>
{
}

public interface IDeleteRoomSeatCommandHandler : IRequestHandler<DeleteRoomSeatCommand, int>
{
}
