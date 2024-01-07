using Application.DataTransferObjects.Theater.Responses;
using Domain.Entities;

namespace Application.DataTransferObjects.Scheduler.Responses;

public class SchedulerFilmAndTheaterResponse 
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal TotalRating { get; set; } = 0;
    public string Location { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public string PhoneNumber { get; set; }
    public string Status { get; set; }
    public bool IsFavorite { get; set; }
}
