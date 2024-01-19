using Application.DataTransferObjects.Film.Responses;
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
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.Repositories.Scheduler;

public class SchedulerRepository : Repository<SchedulerEntity, ApplicationDbContext>, ISchedulerRepository
{
    private readonly DbSet<SchedulerEntity> _schedulerEntities;
    private readonly DbSet<AccountFavoritesEntity> _accountFavoritesEntities;
    private readonly DbSet<FilmFeedbackEntity> _filmFeedbackEntities;
    private readonly DbSet<SeatEntity> _seatEntities;
    private readonly IMapper _mapper;

    public SchedulerRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _schedulerEntities = applicationDbContext.Set<SchedulerEntity>();
        _accountFavoritesEntities = applicationDbContext.Set<AccountFavoritesEntity>();
        _filmFeedbackEntities = applicationDbContext.Set<FilmFeedbackEntity>();
        _seatEntities = applicationDbContext.Set<SeatEntity>();
    }

    public async Task<OffsetPaginationResponse<SchedulerResponse>> GetListSchedulersAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _schedulerEntities
            .Where(x => !x.Deleted)
            .GroupJoin(
                _seatEntities, // The collection to join
                scheduler => scheduler.Id, // Key from the first collection
                seat => seat.SchedulerId, // Key from the second collection
                (scheduler, seats) => new { scheduler, seats } // Result selector
            )
            .Select(group => new SchedulerResponse
            {
                Id = group.scheduler.Id,
                FilmId = group.scheduler.FilmId,
                Film = group.scheduler.Film,
                RoomId = group.scheduler.RoomId,
                Room = group.scheduler.Room,
                StartTime = group.scheduler.StartTime,
                EndTime = group.scheduler.EndTime,
                Status = group.scheduler.Status,
                TheaterId = group.scheduler.TheaterId,
                Theater = group.scheduler.Theater,
                CountSeat = group.seats.Count(), // Counting the seats
                CreatedTime = group.scheduler.CreatedTime
            });

        var response = await query.PaginateAsync<SchedulerEntity, SchedulerResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<SchedulerResponse>
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }


    public async Task<List<long>> GetDistinctTheaterIdsForFilmAsync(long filmId, CancellationToken cancellationToken)
    {
        var distinctTheaterIds = await _schedulerEntities
            .Where(x => !x.Deleted && x.FilmId == filmId)
            .Select(x => x.Theater.Id)
            .Distinct()
            .ToListAsync(cancellationToken);

        return distinctTheaterIds;
    }
    public async Task<OffsetPaginationResponse<SchedulerFilmAndTheaterResponse>> GetListTheaterByFilmAsync(OffsetPaginationRequest request, long filmId, string? tab, long? accountId, CancellationToken cancellationToken)
    {
        var distinctTheaterIds = await GetDistinctTheaterIdsForFilmAsync(filmId, cancellationToken);

        // Use the distinct theater IDs in your main query
        var query = _schedulerEntities
            .Where(x => !x.Deleted && x.FilmId == filmId && DateTimeOffset.UtcNow.AddHours(7) <= x.StartTime.AddMinutes(10))
            .Select(x => new SchedulerFilmAndTheaterResponse()
            {
                Id = x.Theater.Id,
                Status = x.Theater.Status,
                Latitude = x.Theater.Latitude,
                Longitude = x.Theater.Longitude,
                Location = x.Theater.Location,
                Name = x.Theater.Name,
                PhoneNumber = x.Theater.PhoneNumber,
                TotalRating = x.Theater.TotalRating,
                IsFavorite = false
            });

        if (accountId != null)
        {
            query = query
                .GroupJoin(
                    _accountFavoritesEntities.AsNoTracking().Where(x => x.AccountId == accountId),
                    theater => theater.Id,
                    favorite => favorite.TheaterId,
                    (theater, favorites) => new { Theater = theater, Favorites = favorites })
                .SelectMany(
                    x => x.Favorites.DefaultIfEmpty(),
                    (x, favorite) => new SchedulerFilmAndTheaterResponse()
                    {
                        Id = x.Theater.Id,
                        Status = x.Theater.Status,
                        Latitude = x.Theater.Latitude,
                        Longitude = x.Theater.Longitude,
                        Location = x.Theater.Location,
                        Name = x.Theater.Name,
                        PhoneNumber = x.Theater.PhoneNumber,
                        TotalRating = x.Theater.TotalRating,
                        IsFavorite = favorite != null ? true : false
                    });
        }

        if (tab == "Favorites")
        {
            query = query.Where(x => x.IsFavorite);
        }

        var response = await query.Distinct().PaginateAsync<SchedulerEntity, SchedulerFilmAndTheaterResponse>(request, cancellationToken);

        return new OffsetPaginationResponse<SchedulerFilmAndTheaterResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }



    public async Task<SchedulerResponse?> GetSchedulerByIdAsync(long id, CancellationToken cancellationToken)
    {
        var result = await _schedulerEntities.AsNoTracking().ProjectTo<SchedulerResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
        var filmFeedbackResponse = await _filmFeedbackEntities.Where(x => x.FilmId == result.FilmId).GroupBy(x => x.FilmId).Select(x => new FeedbackFilmResponse()
        {
            FilmId = id,
            AverageRating = (double)x.Sum(xx => xx.Rating) / x.Count(),
            CountOneStar = x.Count(xx => xx.Rating == 1),
            CountTwoStar = x.Count(xx => xx.Rating == 2),
            CountThreeStar = x.Count(xx => xx.Rating == 3),
            CountFourStar = x.Count(xx => xx.Rating == 4),
            CountFiveStar = x.Count(xx => xx.Rating == 5),
            CountStart = x.Count()
        }).FirstOrDefaultAsync(cancellationToken);
        result.Feedback = filmFeedbackResponse;
        return result;
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

    public async Task<ICollection<SchedulerGroupResponse>> GetSchedulerByDateAndTheaterIdAsync(long theaterId, string date, CancellationToken cancellationToken)
    {
        var schedules = await _schedulerEntities.AsNoTracking().Include(x => x.Film)
            .Include(x => x.Room)
            .Where(x => x.TheaterId == theaterId && x.StartTime.Day == DateTimeOffset.Parse(date).Day && x.StartTime.Month == DateTimeOffset.Parse(date).Month && x.StartTime.Year == DateTimeOffset.Parse(date).Year && DateTimeOffset.UtcNow.AddHours(7) <= x.StartTime.AddMinutes(10) && x.Status != EntityStatus.Deleted)
            .ToListAsync(cancellationToken);

        var groupedSchedules = schedules.GroupBy(x => x.FilmId);

        var result = groupedSchedules.Select(x => new SchedulerGroupResponse
        {
            SchedulerFilmResponses = new List<SchedulerFilmResponse>
            {
                new SchedulerFilmResponse
                {
                    FilmId = x.Key,
                    Film = x.First().Film, // Assuming Film is a navigation property on the Schedule entity
                    SchedulerRoomResponse = x.GroupBy(s => s.RoomId)
                        .Select(roomGroup => new SchedulerRoomResponse
                        {
                            RoomId = roomGroup.Key,
                            Room = roomGroup.First().Room, // Assuming Room is a navigation property on the Schedule entity
                            SchedulerTimeResponses = roomGroup.Select(schedule => new SchedulerTimeResponse
                            {
                                SchedulerId = schedule.Id,
                                StartTime = schedule.StartTime,
                                EndTime = schedule.EndTime
                            }).ToList()
                        }).ToList()
                }
            }
        }).ToList();

        return result;
    }



    public async Task<ICollection<SchedulerGroupResponse>> GetSchedulerByDateAndTheaterIdAndFilmIdAsync(long theaterId, string date, long filmId, CancellationToken cancellationToken)
    {
        var schedules = await _schedulerEntities.AsNoTracking().Include(x => x.Film)
            .Include(x => x.Room)
            .Where(x => x.FilmId == filmId &&  x.TheaterId == theaterId && x.StartTime.Day == DateTimeOffset.Parse(date).Day && x.StartTime.Month == DateTimeOffset.Parse(date).Month && x.StartTime.Year == DateTimeOffset.Parse(date).Year && DateTimeOffset.UtcNow.AddHours(7) <= x.StartTime.AddMinutes(10) && x.Status != EntityStatus.Deleted)
            .ToListAsync(cancellationToken);

        var groupedSchedules = schedules.GroupBy(x => x.FilmId);

        var result = groupedSchedules.Select(x => new SchedulerGroupResponse
        {
            SchedulerFilmResponses = new List<SchedulerFilmResponse>
            {
                new SchedulerFilmResponse
                {
                    FilmId = x.Key,
                    Film = x.First().Film, // Assuming Film is a navigation property on the Schedule entity
                    SchedulerRoomResponse = x.GroupBy(s => s.RoomId)
                        .Select(roomGroup => new SchedulerRoomResponse
                        {
                            RoomId = roomGroup.Key,
                            Room = roomGroup.First().Room, // Assuming Room is a navigation property on the Schedule entity
                            SchedulerTimeResponses = roomGroup.Select(schedule => new SchedulerTimeResponse
                            {
                                SchedulerId = schedule.Id,
                                StartTime = schedule.StartTime,
                                EndTime = schedule.EndTime
                            }).ToList()
                        }).ToList()
                }
            }
        }).ToList();

        return result;
    }

    public async Task<ICollection<SchedulerGroupResponse>> GetSchedulerByDateAndFilmIdAsync(string date, long filmId, CancellationToken cancellationToken)
    {
        var schedules = await _schedulerEntities.AsNoTracking().Include(x => x.Film)
            .Include(x => x.Room)
            .Include(x => x.Room.Theater)
            .Where(x => x.FilmId == filmId && x.StartTime.Day == DateTimeOffset.Parse(date).Day && x.StartTime.Month == DateTimeOffset.Parse(date).Month && x.StartTime.Year == DateTimeOffset.Parse(date).Year && DateTimeOffset.UtcNow.AddHours(7) <= x.StartTime.AddMinutes(10) && x.Status != EntityStatus.Deleted)
            .ToListAsync(cancellationToken);

        var groupedSchedules = schedules.GroupBy(x => x.FilmId);

        var result = groupedSchedules.Select(x => new SchedulerGroupResponse
        {
            SchedulerFilmResponses = new List<SchedulerFilmResponse>
            {
                new SchedulerFilmResponse
                {
                    FilmId = x.Key,
                    Film = x.First().Film, // Assuming Film is a navigation property on the Schedule entity
                    SchedulerRoomResponse = x.GroupBy(s => s.RoomId)
                        .Select(roomGroup => new SchedulerRoomResponse
                        {
                            RoomId = roomGroup.Key,
                            Room = roomGroup.First().Room, // Assuming Room is a navigation property on the Schedule entity
                            SchedulerTimeResponses = roomGroup.Select(schedule => new SchedulerTimeResponse
                            {
                                SchedulerId = schedule.Id,
                                StartTime = schedule.StartTime,
                                EndTime = schedule.EndTime
                            }).ToList()
                        }).ToList()
                }
            }
        }).ToList();

        return result;
    }
}