using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Booking.Requests;
[ExcludeFromCodeCoverage]
public class UpdateReceivedBookingRequest
{
    public Guid BookingId { get; set; }
}
