using Application.DataTransferObjects.Ticket.Requests;
using MediatR;

namespace Application.Commands.Ticket;

public class CreateTicketCommand : CreateTicketRequest, IRequest<int>
{
    public CreateTicketCommand(Domain.Entities.Ticket entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Ticket Entity { get; set; }
}