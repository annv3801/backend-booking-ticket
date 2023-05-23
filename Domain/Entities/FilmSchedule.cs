using Domain.Common;

namespace Domain.Entities;

public class FilmSchedule : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public Film Film { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Status { get; set; }
}