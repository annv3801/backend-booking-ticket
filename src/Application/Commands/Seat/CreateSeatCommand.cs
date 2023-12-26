using Domain.Entities;
using MediatR;

namespace Application.Commands.Seat;

public class CreateSeatCommand : IRequest<int>
{
    public SeatEntity Entity { get; set; }
}