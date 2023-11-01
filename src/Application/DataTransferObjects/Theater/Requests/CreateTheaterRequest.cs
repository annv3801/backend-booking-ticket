// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Application.DataTransferObjects.Theater.Requests;

public class CreateTheaterRequest
{
    public string Name { get; set; }
    public decimal TotalRating { get; set; } = 0;
    public string Location { get; set; }
    public long Longitude { get; set; }
    public long Latitude { get; set; }
    public long PhoneNumber { get; set; }
}