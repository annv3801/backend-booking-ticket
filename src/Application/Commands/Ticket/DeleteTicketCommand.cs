using MediatR;

namespace Application.Commands.Ticket;

public class DeleteTicketCommand : IRequest<int>
{
    public required long Id { get; set; }
}