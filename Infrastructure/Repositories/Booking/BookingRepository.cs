using Application.Repositories.Booking;
using Domain.Entities;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;

namespace Infrastructure.Repositories.Booking;
public class BookingRepository : Repository<Domain.Entities.Booking>, IBookingRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public BookingRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public Task<Domain.Entities.Booking?> GetBookingAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }

    public async Task AddRangeAsync(IEnumerable<BookingDetail> bookingDetails, CancellationToken cancellationToken = default)
    {
        await _applicationDbContext.Set<BookingDetail>().AddRangeAsync(bookingDetails, cancellationToken);
    }
}
