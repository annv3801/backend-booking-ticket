using Domain.Entities;
using MediatR;

namespace Application.Commands.Seat;

public class CreateListSeatCommand : IRequest<int>
{
    public List<SeatEntity> Entity { get; set; }
}