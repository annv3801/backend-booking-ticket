using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Seat.Requests;
using MediatR;

namespace Application.Commands.Seat;
[ExcludeFromCodeCoverage]
public class UpdateSeatCommand : UpdateSeatRequest, IRequest<int>
{
    public UpdateSeatCommand(Domain.Entities.Seat entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Seat Entity { get; set; }
}
