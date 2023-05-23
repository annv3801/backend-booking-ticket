using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Booking.Requests;

namespace Application.Services.Booking;
public interface IBookingManagementService
{
    Task<Result<BookingResult>> BookingAsync(BookingRequest request, CancellationToken cancellationToken);
}