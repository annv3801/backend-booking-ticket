using Application.Common.Interfaces;
using Application.DataTransferObjects.Booking.Requests;
using Application.Repositories.Booking;
using Domain.Entities;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Booking;
public class BookingRepository : Repository<Domain.Entities.Booking>, IBookingRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ICurrentAccountService _currentAccountService;
    public BookingRepository(ApplicationDbContext applicationDbContext, ICurrentAccountService currentAccountService) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _currentAccountService = currentAccountService;
    }

    public async Task<Domain.Entities.Booking?> GetBookingAsync(Guid bookingId, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Bookings.FirstOrDefaultAsync(x => x.Id == bookingId, cancellationToken);

    }

    public async Task AddRangeAsync(IEnumerable<BookingDetail> bookingDetails, CancellationToken cancellationToken = default)
    {
        await _applicationDbContext.Set<BookingDetail>().AddRangeAsync(bookingDetails, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Booking>> GetListBookingByUserAsync(ViewListBookingByUserRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.Bookings.Where(x => x.CreatedById == _currentAccountService.Id)
            .AsSplitQuery()
            .AsQueryable();
    }

    public async Task<IQueryable<Domain.Entities.Booking>> GetListBookingByAdminAsync(ViewListBookingByAdminRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.Bookings
            .AsSplitQuery()
            .AsQueryable();
    }
    public async Task<List<BookingDetail>> GetListBookingDetailAsync(Guid bookingId, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return await _applicationDbContext.BookingDetails.Where(x => x.BookingId == bookingId).ToListAsync(cancellationToken);
    }
}
