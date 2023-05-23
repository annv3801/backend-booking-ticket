using Application.Commands.Ticket;
using MediatR;

namespace Application.Handlers.Ticket;

public interface IUpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, int>
{
}