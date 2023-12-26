using MediatR;

namespace Application.Queries.Ticket;

public class CheckDuplicatedTicketByNameQuery :  IRequest<bool>
{
    public string Title { get; set; }
}