using MediatR;

namespace Application.Queries.Ticket;

public class CheckDuplicatedTicketByNameAndIdQuery : IRequest<bool>
{
    public string Title { get; set; }
    public long Id { get; set; }
}