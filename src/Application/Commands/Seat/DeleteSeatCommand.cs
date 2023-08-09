using MediatR;

namespace Application.Commands.Seat;

public class DeleteSeatCommand : IRequest<int>
{
    public DeleteSeatCommand(Domain.Entities.Seat entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Seat Entity { get; set; }
}