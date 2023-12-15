using Domain.Common.Entities;
using Domain.Constants;

namespace Domain.Entities;

public class TheaterEntity : Entity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal TotalRating { get; set; } = 0;
    public string Location { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public string PhoneNumber { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}