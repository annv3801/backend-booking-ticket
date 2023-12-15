using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Theater.Requests;

[ExcludeFromCodeCoverage]
public class UpdateTheaterRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal TotalRating { get; set; } = 0;
    public string Location { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public string PhoneNumber { get; set; }
}