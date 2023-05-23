namespace Application.DataTransferObjects.FilmSchedules.Responses;

public class TheaterScheduleResponse
{
    public Guid TheaterId { get; set; }
    public string TheaterName { get; set; }
    public List<ScheduleResponse> ListSchedule { get; set; }
}