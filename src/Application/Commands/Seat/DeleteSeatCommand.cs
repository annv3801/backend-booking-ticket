using MediatR;

namespace Application.Commands.Seat;

public class DeleteSeatCommand : IRequest<int>
{
    public long Id { get; set; }
}