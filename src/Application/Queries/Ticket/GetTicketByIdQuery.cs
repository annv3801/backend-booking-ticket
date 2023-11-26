using Application.DataTransferObjects.Ticket.Responses;
using MediatR;

namespace Application.Queries.Ticket;

public class GetTicketByIdQuery : IRequest<TicketResponse?>
{
    public long Id { get; set; }
}