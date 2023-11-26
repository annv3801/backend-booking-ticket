using Domain.Entities;
using MediatR;

namespace Application.Commands.Ticket;

public class CreateTicketCommand : IRequest<int>
{
    public required TicketEntity Entity { get; set; }
}