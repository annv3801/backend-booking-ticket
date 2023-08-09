using Application.Commands.Ticket;
using MediatR;

namespace Application.Handlers.Ticket;

public interface IDeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, int>
{
}