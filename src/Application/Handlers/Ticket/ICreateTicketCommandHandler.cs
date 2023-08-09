using Application.Commands.Ticket;
using MediatR;

namespace Application.Handlers.Ticket;

public interface ICreateTicketCommandHandler: IRequestHandler<CreateTicketCommand, int>
{
    
}