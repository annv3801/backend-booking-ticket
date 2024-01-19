using Domain.Constants;

namespace Application.DataTransferObjects.Ticket.Responses;

public class TicketResponse 
{
    public long Id { get; set; }
    public string Title { get; set; }
    public int Type { get; set; }
    public long Price { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
    public string Color { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
}
