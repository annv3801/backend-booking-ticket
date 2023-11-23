using System.Reflection.PortableExecutable;
using Application.DataTransferObjects.Scheduler.Responses;
using Application.Interface;
using Application.Repositories.Scheduler;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Scheduler;

public class SchedulerRepository : Repository<SchedulerEntity, ApplicationDbContext>, ISchedulerRepository
{
    private readonly DbSet<SchedulerEntity> _schedulerEntities;
    private readonly IMapper _mapper;

    public SchedulerRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _schedulerEntities = applicationDbContext.Set<SchedulerEntity>();
    }

    public async Task<OffsetPaginationResponse<SchedulerResponse>> GetListSchedulersAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _schedulerEntities.Where(x => !x.Deleted).Select(x => new SchedulerResponse()
        {
            Id = x.Id,
            FilmId = x.FilmId,
            Film = x.Film,
            RoomId = x.RoomId,
            Room = x.Room,
            StartTime = x.StartTime,
            EndTime = x.EndTime,
            Status = x.Status,
        });

        var response = await query.PaginateAsync<SchedulerEntity, SchedulerResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<SchedulerResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<SchedulerResponse?> GetSchedulerByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _schedulerEntities.AsNoTracking().ProjectTo<SchedulerResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SchedulerEntity?> GetSchedulerEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _schedulerEntities.AsNoTracking().Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SchedulerResponse?> GetSchedulerByTime(long roomId, DateTimeOffset startTime, CancellationToken cancellationToken)
    {
        return await _schedulerEntities.AsNoTracking()
            .ProjectTo<SchedulerResponse>(_mapper.ConfigurationProvider)
            .Where(x => x.RoomId == roomId && x.StartTime <= startTime && startTime <= x.EndTime && x.Status != EntityStatus.Deleted)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<SchedulerResponse>> GetSchedulerByDateAndTheaterIdAsync(long theaterId, string date, CancellationToken cancellationToken)
    {
        return await _schedulerEntities.AsNoTracking()
            .ProjectTo<SchedulerResponse>(_mapper.ConfigurationProvider)
            .Where(x => x.TheaterId == theaterId && x.StartTime.Day == DateTimeOffset.Parse(date).Day && x.StartTime.Month == DateTimeOffset.Parse(date).Month && x.StartTime.Year == DateTimeOffset.Parse(date).Year && DateTimeOffset.UtcNow.AddHours(7) <= x.StartTime.AddMinutes(10) && x.Status != EntityStatus.Deleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<SchedulerResponse>> GetSchedulerByDateAndTheaterIdAndFilmIdAsync(long theaterId, string date, long filmId, CancellationToken cancellationToken)
    {
        return await _schedulerEntities.AsNoTracking()
            .ProjectTo<SchedulerResponse>(_mapper.ConfigurationProvider)
            .Where(x => x.TheaterId == theaterId 
                        && x.StartTime.Day == DateTimeOffset.Parse(date).Day 
                        && x.StartTime.Month == DateTimeOffset.Parse(date).Month 
                        && x.StartTime.Year == DateTimeOffset.Parse(date).Year 
                        && DateTimeOffset.UtcNow.AddHours(7) <= x.StartTime.AddMinutes(10) 
                        && x.FilmId == filmId 
                        && x.Status != EntityStatus.Deleted)
            .ToListAsync(cancellationToken);
    }
}