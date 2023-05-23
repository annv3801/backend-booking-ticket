using Application.Commands.Seat;
using MediatR;

namespace Application.Handlers.Seat;

public interface ICreateSeatCommandHandler: IRequestHandler<CreateSeatCommand, int>
{
    
}