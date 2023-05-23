using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Booking.Requests;
[ExcludeFromCodeCoverage]
public class BookingRequest
{
    public Guid ScheduleId { get; set; }
    public List<Guid> SeatId { get; set; }
    public double Total { get; set; }
    public string? CouponId { get; set; }
    public double TotalBeforeDiscount { get; set; }
    public double Discount { get; set; }
    public int PaymentMethod { get; set; }
}
