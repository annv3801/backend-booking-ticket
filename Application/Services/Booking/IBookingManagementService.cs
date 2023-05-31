using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
using Application.DataTransferObjects.Pagination.Responses;

namespace Application.Services.Booking;
public interface IBookingManagementService
{
    Task<Result<BookingResult>> BookingAsync(BookingRequest request, CancellationToken cancellationToken);
    Task<Result<PaginationBaseResponse<BookingResponse>>> ViewListBookingsByUserAsync(ViewListBookingByUserRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<BookingResponse>>> ViewListBookingsByAdminAsync(ViewListBookingByAdminRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<BookingResult>> UpdateReceivedBookingAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

}