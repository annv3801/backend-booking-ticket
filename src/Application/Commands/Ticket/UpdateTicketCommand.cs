using Domain.Entities;
using MediatR;

namespace Application.Commands.Ticket;

public class UpdateTicketCommand : IRequest<int>
{
    public required TicketEntity Request { get; set; }
}