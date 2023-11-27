using Application.Commands.Seat;
using MediatR;

namespace Application.Handlers.Seat;

public interface ICreateSeatCommandHandler : IRequestHandler<CreateSeatCommand, int>
{
}

public interface IUpdateSeatCommandHandler : IRequestHandler<UpdateSeatCommand, int>
{
}

public interface IDeleteSeatCommandHandler : IRequestHandler<DeleteSeatCommand, int>
{
}
