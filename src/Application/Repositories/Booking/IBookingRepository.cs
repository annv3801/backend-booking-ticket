using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Booking;
public interface IBookingRepository : IRepository<BookingEntity>
{
    Task<OffsetPaginationResponse<BookingResponse>> GetListBookingsAsync(ViewListBookingsRequest request, CancellationToken cancellationToken);
    Task<BookingResponse?> GetBookingByIdAsync(long id, CancellationToken cancellationToken);
    Task<BookingEntity?> GetBookingEntityByIdAsync(long id, CancellationToken cancellationToken);
}
