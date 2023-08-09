using MediatR;

namespace Application.Commands.Ticket;

public class DeleteTicketCommand : IRequest<int>
{
    public DeleteTicketCommand(Domain.Entities.Ticket entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Ticket Entity { get; set; }
}