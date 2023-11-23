using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Scheduler.Requests;

[ExcludeFromCodeCoverage]
public class UpdateSchedulerRequest
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public long FilmId { get; set; }
    public long TheaterId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}