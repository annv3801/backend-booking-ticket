using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Ticket.Responses;
[ExcludeFromCodeCoverage]
public class ViewTicketResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ScheduleId { get; set; }
    public float? Price { get; set; }
    public int Type { get; set; }
}
