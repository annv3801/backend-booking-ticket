using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Booking.Responses;
[ExcludeFromCodeCoverage]
public class BookingResponse
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string? PhoneNumber { get; set; }
    public int PaymentMethod { get; set; }
    public double Total { get; set; }
    public int IsReceived { get; set; }
    public string ListSeatName { get; set; }
}
