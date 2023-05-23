namespace Application.DataTransferObjects.Ticket.Requests;

public class CreateTicketRequest
{
    public string Name { get; set; }
    public Guid ScheduleId { get; set; }
    public float? Price { get; set; }
    public int Type { get; set; }
}