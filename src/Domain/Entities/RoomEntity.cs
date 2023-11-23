using Domain.Common.Entities;
using Domain.Constants;

namespace Domain.Entities;

public class RoomEntity : Entity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
    public long TheaterId { get; set; }
    public TheaterEntity Theater { get; set; }
}