using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Ticket.Requests;
using MediatR;

namespace Application.Commands.Ticket;
[ExcludeFromCodeCoverage]
public class UpdateTicketCommand : UpdateTicketRequest, IRequest<int>
{
    public UpdateTicketCommand(Domain.Entities.Ticket entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Ticket Entity { get; set; }
}
