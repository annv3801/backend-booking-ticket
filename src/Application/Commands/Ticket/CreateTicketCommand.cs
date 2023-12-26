using Domain.Entities;
using MediatR;

namespace Application.Commands.Ticket;

public class CreateTicketCommand : IRequest<int>
{
    public TicketEntity Entity { get; set; }
}