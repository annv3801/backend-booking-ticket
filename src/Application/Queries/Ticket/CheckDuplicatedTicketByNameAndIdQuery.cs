using MediatR;

namespace Application.Queries.Ticket;

public class CheckDuplicatedTicketByNameAndIdQuery : IRequest<bool>
{
    public required string Title { get; set; }
    public required long Id { get; set; }
}