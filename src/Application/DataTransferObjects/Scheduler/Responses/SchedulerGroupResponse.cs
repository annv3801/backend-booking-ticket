using Domain.Entities;

namespace Application.DataTransferObjects.Scheduler.Responses;

public class SchedulerGroupResponse 
{
    public List<SchedulerFilmResponse> SchedulerFilmResponses { get; set; }
}

public class SchedulerFilmResponse
{
    public long FilmId { get; set; }
    public FilmEntity Film { get; set; }
    public List<SchedulerRoomResponse> SchedulerRoomResponse { get; set; }
}

public class SchedulerRoomResponse
{
    public long RoomId { get; set; }
    public RoomEntity Room { get; set; }
    public List<SchedulerTimeResponse> SchedulerTimeResponses { get; set; }
}

public class SchedulerTimeResponse
{
    public long SchedulerId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}