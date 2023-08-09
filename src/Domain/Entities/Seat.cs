using Domain.Common;

namespace Domain.Entities;

public class Seat : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid ScheduleId { get; set; }
    public FilmSchedule Schedule { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }
}