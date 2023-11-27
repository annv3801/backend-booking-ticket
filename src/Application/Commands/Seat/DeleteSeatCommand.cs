using MediatR;

namespace Application.Commands.Seat;

public class DeleteSeatCommand : IRequest<int>
{
    public required long Id { get; set; }
}