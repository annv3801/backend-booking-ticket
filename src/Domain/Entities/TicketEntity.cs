using Domain.Common.Entities;
using Domain.Constants;

namespace Domain.Entities;

public class TicketEntity : Entity<long>
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public int Type { get; set; }
    public required string Price { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}