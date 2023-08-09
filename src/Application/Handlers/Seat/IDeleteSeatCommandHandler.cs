using Application.Commands.Seat;
using MediatR;

namespace Application.Handlers.Seat;

public interface IDeleteSeatCommandHandler : IRequestHandler<DeleteSeatCommand, int>
{
}