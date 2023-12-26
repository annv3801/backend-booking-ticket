using Domain.Common.Entities;
using Domain.Constants;

namespace Domain.Entities;

public class TicketEntity : Entity<long>
{
    public long Id { get; set; }
    public string Title { get; set; }
    public int Type { get; set; }
    public long Price { get; set; }
    public string Color { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}