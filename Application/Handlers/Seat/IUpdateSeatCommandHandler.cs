using Application.Commands.Seat;
using MediatR;

namespace Application.Handlers.Seat;

public interface IUpdateSeatCommandHandler : IRequestHandler<UpdateSeatCommand, int>
{
}