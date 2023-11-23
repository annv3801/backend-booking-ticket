using Domain.Entities;

namespace Application.DataTransferObjects.Scheduler.Responses;

public class SchedulerResponse 
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public RoomEntity Room { get; set; }
    public long FilmId { get; set; }
    public FilmEntity Film { get; set; }
    public long TheaterId { get; set; }
    public TheaterEntity Theater { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
    public string Status { get; set; }
}
