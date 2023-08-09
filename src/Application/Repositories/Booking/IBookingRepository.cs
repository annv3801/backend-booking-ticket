using Application.Common.Interfaces;
using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
using Domain.Entities;

namespace Application.Repositories.Booking;
public interface IBookingRepository : IRepository<Domain.Entities.Booking>
{
    Task<Domain.Entities.Booking?> GetBookingAsync(Guid bookingId, CancellationToken cancellationToken = default(CancellationToken));
    Task AddRangeAsync(IEnumerable<BookingDetail> bookingDetails, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.Booking>> GetListBookingByUserAsync(ViewListBookingByUserRequest request, CancellationToken cancellationToken);
    Task<IQueryable<Domain.Entities.Booking>> GetListBookingByAdminAsync(ViewListBookingByAdminRequest request, CancellationToken cancellationToken);
    Task<List<BookingDetail>> GetListBookingDetailAsync(Guid bookingId, CancellationToken cancellationToken);

}
