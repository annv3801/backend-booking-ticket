namespace Application.DataTransferObjects.FilmSchedules.Requests;

public class CreateFilmSchedulesRequest
{
    public Guid FilmId { get; set; }
    public Guid RoomId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Status { get; set; }
}