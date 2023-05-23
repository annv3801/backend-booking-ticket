using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.FilmSchedules.Responses;
[ExcludeFromCodeCoverage]
public class ViewFilmSchedulesResponse
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public string FilmName { get; set; }
    public Guid TheaterId { get; set; }
    public string TheaterName { get; set; }
    public Guid RoomId { get; set; }
    public string RoomName { get; set; }
    public int CountSeat { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Status { get; set; }
}
