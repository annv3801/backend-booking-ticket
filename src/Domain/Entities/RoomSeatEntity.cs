using Domain.Common.Entities;
using Domain.Constants;

namespace Domain.Entities;

public class RoomSeatEntity : Entity<long>
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
    public long RoomId { get; set; }
    public RoomEntity Room { get; set; }
}