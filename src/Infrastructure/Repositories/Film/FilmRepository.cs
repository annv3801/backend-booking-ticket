using Application.DataTransferObjects.Film.Requests;
using Application.DataTransferObjects.Film.Responses;
using Application.Interface;
using Application.Repositories.Film;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Film;

public class FilmRepository : Repository<FilmEntity, ApplicationDbContext>, IFilmRepository
{
    private readonly DbSet<FilmEntity> _filmEntities;
    private readonly DbSet<AccountFavoritesEntity> _accountFavoritesEntities;
    private readonly IMapper _mapper;
    

    public FilmRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _filmEntities = applicationDbContext.Set<FilmEntity>();
        _accountFavoritesEntities = applicationDbContext.Set<AccountFavoritesEntity>();
    }

    public async Task<OffsetPaginationResponse<FilmResponse>> GetListFilmsByGroupsAsync(ViewListFilmsByGroupRequest request, long? accountId, CancellationToken cancellationToken)
    {
        var query = _filmEntities.Where(x => !x.Deleted && x.GroupEntityId == request.GroupId).OrderBy(x => x.Name.ToLower()).Select(x => new FilmResponse()
            {
                Id = x.Id,
                Name = x.Name,
                TotalRating = x.TotalRating,
                Slug = x.Slug,
                Actor = x.Actor,
                Description = x.Description,
                Director = x.Director,
                Duration = x.Duration,
                Genre = x.Genre,
                Image = x.Image,
                Language = x.Language,
                Premiere = x.Premiere,
                Rated = x.Rated,
                Trailer = x.Trailer,
                Group = x.GroupEntityId,
                CategoryIds = x.CategoryIds,
                Status = x.Status,
            });
        if (accountId != 0)
        {
            query = query
                .GroupJoin(
                    _accountFavoritesEntities.AsNoTracking().Where(x => x.AccountId == accountId),  // Assuming _accountFavoriteRepository.GetAccountFavorites(accountId) returns IQueryable<AccountFavorite>
                    film => film.Id,
                    favorite => favorite.FilmId,
                    (film, favorites) => new { Film = film, Favorites = favorites })
                .SelectMany(
                    x => x.Favorites.DefaultIfEmpty(),
                    (x, favorite) => new FilmResponse()
                    {
                        Id = x.Film.Id,
                        Name = x.Film.Name,
                        TotalRating = x.Film.TotalRating,
                        Slug = x.Film.Slug,
                        Actor = x.Film.Actor,
                        Description = x.Film.Description,
                        Director = x.Film.Director,
                        Duration = x.Film.Duration,
                        Genre = x.Film.Genre,
                        Image = x.Film.Image,
                        Language = x.Film.Language,
                        Premiere = x.Film.Premiere,
                        Rated = x.Film.Rated,
                        Trailer = x.Film.Trailer,
                        Group = x.Film.Group,
                        CategoryIds = x.Film.CategoryIds,
                        Status = x.Film.Status,
                        IsFavorite = (favorite != null)
                    });
        }

        var response = await query.PaginateAsync<FilmEntity, FilmResponse>(request, cancellationToken);

        return new OffsetPaginationResponse<FilmResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<OffsetPaginationResponse<FilmResponse>> GetListFilmsFavoritesByAccountAsync(ViewListFilmsFavoriteByAccountRequest request, long accountId, CancellationToken cancellationToken)
    {
        var query = _filmEntities.Where(x => !x.Deleted)
            .OrderBy(x => x.Name.ToLower())
            .Join(
                _accountFavoritesEntities.AsNoTracking().Where(x => x.AccountId == accountId),
                film => film.Id,
                favorite => favorite.FilmId,
                (film, favorite) => new FilmResponse()
                {
                    Id = film.Id,
                    Name = film.Name,
                    TotalRating = film.TotalRating,
                    Slug = film.Slug,
                    Actor = film.Actor,
                    Description = film.Description,
                    Director = film.Director,
                    Duration = film.Duration,
                    Genre = film.Genre,
                    Image = film.Image,
                    Language = film.Language,
                    Premiere = film.Premiere,
                    Rated = film.Rated,
                    Trailer = film.Trailer,
                    Group = film.GroupEntityId,
                    CategoryIds = film.CategoryIds,
                    Status = film.Status,
                    IsFavorite = true
                });
        
        var response = await query.PaginateAsync<FilmEntity, FilmResponse>(request, cancellationToken);

        return new OffsetPaginationResponse<FilmResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<OffsetPaginationResponse<FilmResponse>> GetListFilmsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _filmEntities.Where(x => !x.Deleted).OrderBy(x => x.Name.ToLower()).Select(x => new FilmResponse()
        {
            Id = x.Id,
            Name = x.Name,
            TotalRating = x.TotalRating,
            Slug = x.Slug,
            Actor = x.Actor,
            Description = x.Description,
            Director = x.Director,
            Duration = x.Duration,
            Genre = x.Genre,
            Image = x.Image,
            Language = x.Language,
            Premiere = x.Premiere,
            Rated = x.Rated,
            Trailer = x.Trailer,
            Group = x.GroupEntityId,
            CategoryIds = x.CategoryIds,
            Status = x.Status,
        });
        
        var response = await query.PaginateAsync<FilmEntity,FilmResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<FilmResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<FilmResponse?> GetFilmByIdAsync(long id, long? accountId, CancellationToken cancellationToken)
    {
        var query = _filmEntities
            .AsNoTracking()
            .Where(x => x.Id == id && x.Status != EntityStatus.Deleted)
            .ProjectTo<FilmResponse>(_mapper.ConfigurationProvider);

        if (accountId != 0)
        {
            query = query
                .GroupJoin(
                    _accountFavoritesEntities.AsNoTracking().Where(x => x.AccountId == accountId),
                    film => film.Id,
                    favorite => favorite.FilmId,
                    (film, favorites) => new { Film = film, Favorites = favorites })
                .SelectMany(
                    x => x.Favorites.DefaultIfEmpty(),
                    (x, favorite) => new FilmResponse()
                    {
                        Id = x.Film.Id,
                        Name = x.Film.Name,
                        TotalRating = x.Film.TotalRating,
                        Slug = x.Film.Slug,
                        Actor = x.Film.Actor,
                        Description = x.Film.Description,
                        Director = x.Film.Director,
                        Duration = x.Film.Duration,
                        Genre = x.Film.Genre,
                        Image = x.Film.Image,
                        Language = x.Film.Language,
                        Premiere = x.Film.Premiere,
                        Rated = x.Film.Rated,
                        Trailer = x.Film.Trailer,
                        Group = x.Film.Group,
                        CategoryIds = x.Film.CategoryIds,
                        Status = x.Film.Status,
                        IsFavorite = (favorite != null)
                    });
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<FilmResponse?> GetFilmBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        return await _filmEntities.AsNoTracking().ProjectTo<FilmResponse>(_mapper.ConfigurationProvider).Where(x => x.Slug == slug && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }
    

    public async Task<FilmEntity?> GetFilmEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _filmEntities.AsNoTracking().Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }
}