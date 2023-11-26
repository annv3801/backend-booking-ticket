using Application.Commands.Ticket;
using MediatR;

namespace Application.Handlers.Ticket;

public interface ICreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
{
}

public interface IUpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, int>
{
}

public interface IDeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, int>
{
}
