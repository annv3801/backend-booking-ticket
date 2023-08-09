using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Pagination.Requests;

namespace Application.DataTransferObjects.Booking.Requests;
[ExcludeFromCodeCoverage]
public class ViewListBookingDetailRequest
{
    public Guid BookingId { get; set; }
}
