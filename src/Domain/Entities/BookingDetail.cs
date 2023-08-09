using Domain.Common;

namespace Domain.Entities;

public class BookingDetail : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; }
    public Guid ScheduleId { get; set; }
    public FilmSchedule Schedule { get; set; }
    public Guid SeatId { get; set; }
}