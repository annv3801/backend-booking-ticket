using MediatR;

namespace Application.Queries.Ticket;

public class CheckDuplicatedTicketByNameQuery :  IRequest<bool>
{
    public required string Title { get; set; }
}