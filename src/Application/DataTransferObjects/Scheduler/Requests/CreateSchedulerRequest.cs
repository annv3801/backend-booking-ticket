// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Application.DataTransferObjects.Scheduler.Requests;

public class CreateSchedulerRequest
{
    public long RoomId { get; set; }
    public long FilmId { get; set; }
    public long TheaterId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}