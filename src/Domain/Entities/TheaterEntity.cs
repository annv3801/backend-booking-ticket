using Domain.Common.Entities;
using Domain.Constants;

namespace Domain.Entities;

public class TheaterEntity : Entity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal TotalRating { get; set; } = 0;
    public string Location { get; set; }
    public long Longitude { get; set; }
    public long Latitude { get; set; }
    public long PhoneNumber { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}