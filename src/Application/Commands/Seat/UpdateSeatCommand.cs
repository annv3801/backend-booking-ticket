using Domain.Entities;
using MediatR;

namespace Application.Commands.Seat;

public class UpdateSeatCommand : IRequest<int>
{
    public required SeatEntity Request { get; set; }
}