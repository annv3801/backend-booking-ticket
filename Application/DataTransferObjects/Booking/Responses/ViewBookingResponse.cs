using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Booking.Responses;
[ExcludeFromCodeCoverage]
public class BookingResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ScheduleId { get; set; }
    public float? Price { get; set; }
    public int Type { get; set; }
}
