using Application.DataTransferObjects.Scheduler.Responses;
using Application.DataTransferObjects.Seat.Responses;
using Application.Interface;
using Application.Repositories.Seat;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Seat;

public class SeatRepository : Repository<SeatEntity, ApplicationDbContext>, ISeatRepository
{
    private readonly DbSet<SeatEntity> _seatEntities;
    private readonly IMapper _mapper;

    public SeatRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _seatEntities = applicationDbContext.Set<SeatEntity>();
    }

    public async Task<OffsetPaginationResponse<SeatResponse>> GetListSeatsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _seatEntities.Where(x => !x.Deleted).Select(x => new SeatResponse()
            {
                Status = x.Status,
                Id = x.Id,
                SchedulerId = x.SchedulerId,
                Scheduler = x.Scheduler,
                RoomsSeatId = x.RoomSeatId,
                RoomSeat = x.RoomSeat
            });
        
        var response = await query.PaginateAsync<SeatEntity,SeatResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<SeatResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<ICollection<SeatResponse>> GetListSeatsBySchedulerAsync(long schedulerId, CancellationToken cancellationToken)
    {
        return await _seatEntities.Where(x => x.SchedulerId == schedulerId).ProjectTo<SeatResponse>(_mapper.ConfigurationProvider).Select(x => new SeatResponse()
        {
            Status = x.Status,
            Id = x.Id,
            SchedulerId = x.SchedulerId,
            Ticket = x.Ticket,
            TicketId = x.TicketId,
        }).ToListAsync(cancellationToken);
    }

    public async Task<SeatResponse?> GetSeatByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _seatEntities.AsNoTracking()
            .ProjectTo<SeatResponse>(_mapper.ConfigurationProvider)
            .Where(x => x.Id == id && x.Status != EntityStatus.Deleted)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SeatEntity?> GetSeatEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _seatEntities.AsNoTracking()
            .Where(x => x.Id == id && x.Status != EntityStatus.Deleted)
            .Include(x => x.RoomSeat)
            .Include(x => x.Ticket)
            .Include(x => x.Scheduler)
            .FirstOrDefaultAsync(cancellationToken);
    }
}