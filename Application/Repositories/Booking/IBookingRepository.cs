using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Repositories.Booking;
public interface IBookingRepository : IRepository<Domain.Entities.Booking>
{
    Task<Domain.Entities.Booking?> GetBookingAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken));
    Task AddRangeAsync(IEnumerable<BookingDetail> bookingDetails, CancellationToken cancellationToken = default(CancellationToken));
}
