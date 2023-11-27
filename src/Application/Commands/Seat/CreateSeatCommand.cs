using Domain.Entities;
using MediatR;

namespace Application.Commands.Seat;

public class CreateSeatCommand : IRequest<int>
{
    public required SeatEntity Entity { get; set; }
}