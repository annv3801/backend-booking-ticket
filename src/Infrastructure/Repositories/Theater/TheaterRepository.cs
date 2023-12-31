using Application.DataTransferObjects.Theater.Responses;
using Application.Interface;
using Application.Repositories.Theater;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Theater;

public class TheaterRepository : Repository<TheaterEntity, ApplicationDbContext>, ITheaterRepository
{
    private readonly DbSet<TheaterEntity> _theaterEntities;
    private readonly DbSet<AccountFavoritesEntity> _accountFavoritesEntities;
    private readonly IMapper _mapper;

    public TheaterRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _theaterEntities = applicationDbContext.Set<TheaterEntity>();
        _accountFavoritesEntities = applicationDbContext.Set<AccountFavoritesEntity>();
    }

    public async Task<OffsetPaginationResponse<TheaterResponse>> GetListTheatersAsync(OffsetPaginationRequest request, long? accountId, CancellationToken cancellationToken)
    {
        var query = _theaterEntities.Where(x => !x.Deleted).OrderBy(x => x.Name.ToLower()).Select(x => new TheaterResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Latitude = x.Latitude,
                TotalRating = x.TotalRating,
                Longitude = x.Longitude,
                Location = x.Location,
                PhoneNumber = x.PhoneNumber,
                Status = x.Status,
            });
        
        if (accountId != 0)
        {
            query = query
                .GroupJoin(
                    _accountFavoritesEntities.AsNoTracking().Where(x => x.AccountId == accountId),  // Assuming _accountFavoriteRepository.GetAccountFavorites(accountId) returns IQueryable<AccountFavorite>
                    theater => theater.Id,
                    favorite => favorite.TheaterId,
                    (theater, favorites) => new { Theater = theater, Favorites = favorites })
                .SelectMany(
                    x => x.Favorites.DefaultIfEmpty(),
                    (x, favorite) => new TheaterResponse()
                    {
                        Id = x.Theater.Id,
                        Name = x.Theater.Name,
                        Latitude = x.Theater.Latitude,
                        TotalRating = x.Theater.TotalRating,
                        Longitude = x.Theater.Longitude,
                        Location = x.Theater.Location,
                        PhoneNumber = x.Theater.PhoneNumber,
                        Status = x.Theater.Status,
                        IsFavorite = (favorite != null)
                    });
        }
        
        var response = await query.PaginateAsync<TheaterEntity,TheaterResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<TheaterResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<TheaterResponse?> GetTheaterByIdAsync(long id, long? accountId, CancellationToken cancellationToken)
    {
        var query = _theaterEntities
            .AsNoTracking()
            .Where(x => x.Id == id && x.Status != EntityStatus.Deleted)
            .ProjectTo<TheaterResponse>(_mapper.ConfigurationProvider);

        if (accountId != 0)
        {
            query = query
                .GroupJoin(
                    _accountFavoritesEntities.AsNoTracking().Where(x => x.AccountId == accountId.Value),
                    theater => theater.Id,
                    favorite => favorite.TheaterId,
                    (theater, favorites) => new { Theater = theater, Favorites = favorites })
                .SelectMany(
                    x => x.Favorites.DefaultIfEmpty(),
                    (x, favorite) => new TheaterResponse()
                    {
                        Id = x.Theater.Id,
                        Name = x.Theater.Name,
                        Latitude = x.Theater.Latitude,
                        TotalRating = x.Theater.TotalRating,
                        Longitude = x.Theater.Longitude,
                        Location = x.Theater.Location,
                        PhoneNumber = x.Theater.PhoneNumber,
                        Status = x.Theater.Status,
                        IsFavorite = (favorite != null)
                    });
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TheaterEntity?> GetTheaterEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _theaterEntities.AsNoTracking().Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsDuplicatedTheaterByNameAndIdAsync(string name, long id, CancellationToken cancellationToken)
    {
        return await _theaterEntities.AsNoTracking().AnyAsync(x => x.Name == name && x.Id != id && !x.Deleted, cancellationToken);
    }

    public async Task<bool> IsDuplicatedTheaterByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _theaterEntities.AsNoTracking().AnyAsync(x => x.Name == name && !x.Deleted, cancellationToken);
    }
    
}