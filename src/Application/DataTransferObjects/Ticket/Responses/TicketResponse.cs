using Domain.Constants;

namespace Application.DataTransferObjects.Ticket.Responses;

public class TicketResponse 
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public int Type { get; set; }
    public required string Price { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}