using Application.DataTransferObjects.Seat.Requests;
using Application.Queries.Seat;
using Application.Repositories.Seat;
using AutoMapper;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Seat;

public class SeatRepository : Repository<Domain.Entities.Seat>, ISeatRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public SeatRepository(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<Domain.Entities.Seat?> GetSeatByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Seats.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Seat>> GetListSeatsAsync(ViewListSeatsRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.Seats
            .AsSplitQuery()
            .AsQueryable();
    }

    public async Task<IQueryable<Domain.Entities.Seat>> ViewListSeatsByScheduleAsync(ViewListSeatByScheduleQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.Seats
            .AsSplitQuery()
            .AsQueryable();
    }
}