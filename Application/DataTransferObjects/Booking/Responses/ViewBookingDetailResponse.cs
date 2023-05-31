using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Booking.Responses;
[ExcludeFromCodeCoverage]
public class BookingDetailResponse
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Guid SeatId { get; set; }
    public string SeatName { get; set; }
}
