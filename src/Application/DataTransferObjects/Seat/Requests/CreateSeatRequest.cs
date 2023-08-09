namespace Application.DataTransferObjects.Seat.Requests;

public class CreateSeatRequest
{
    public string Name { get; set; }
    public Guid ScheduleId { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }
}