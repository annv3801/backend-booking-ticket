using MediatR;

namespace Application.Commands.Ticket;

public class DeleteTicketCommand : IRequest<int>
{
    public long Id { get; set; }
}