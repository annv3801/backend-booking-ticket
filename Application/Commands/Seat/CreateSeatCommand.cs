using Application.DataTransferObjects.Seat.Requests;
using MediatR;

namespace Application.Commands.Seat;

public class CreateSeatCommand : CreateSeatRequest, IRequest<int>
{
    public CreateSeatCommand(Domain.Entities.Seat entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Seat Entity { get; set; }
}