using Domain.Common.Entities;
using Domain.Constants;

namespace Domain.Entities;

public class SeatEntity : Entity<long>
{
    public long Id { get; set; }
    public long RoomSeatId { get; set; }
    public RoomSeatEntity RoomSeat { get; set; }
    public long SchedulerId { get; set; }
    public SchedulerEntity Scheduler { get; set; }
    public long TicketId { get; set; }
    public TicketEntity Ticket { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
    public int Type { get; set; }
}