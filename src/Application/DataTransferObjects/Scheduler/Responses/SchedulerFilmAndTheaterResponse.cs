using Application.DataTransferObjects.Theater.Responses;
using Domain.Entities;

namespace Application.DataTransferObjects.Scheduler.Responses;

public class SchedulerFilmAndTheaterResponse 
{
    public long FilmId { get; set; }
    public FilmEntity Film { get; set; }
    public List<TheaterResponse> ListTheaters { get; set; }
}
