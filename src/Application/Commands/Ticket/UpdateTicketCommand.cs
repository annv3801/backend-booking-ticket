using Domain.Entities;
using MediatR;

namespace Application.Commands.Ticket;

public class UpdateTicketCommand : IRequest<int>
{
    public TicketEntity Request { get; set; }
}