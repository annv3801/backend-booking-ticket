using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Booking;

public interface IBookingManagementService
{
    Task<RequestResult<bool>> CreateBookingAsync(CreateBookingRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> CancelBookingAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<bool>> ChangeStatusBookingAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<BookingResponse>> GetBookingAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<BookingResponse>>> GetListBookingsAsync(ViewListBookingsRequest request, CancellationToken cancellationToken);
}