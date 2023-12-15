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
    private readonly IMapper _mapper;

    public FilmRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _filmEntities = applicationDbContext.Set<FilmEntity>();
    }

    public async Task<OffsetPaginationResponse<FilmResponse>> GetListFilmsByGroupsAsync(ViewListFilmsByGroupRequest request, CancellationToken cancellationToken)
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
        
        var response = await query.PaginateAsync<FilmEntity,FilmResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<FilmResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<FilmResponse?> GetFilmByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _filmEntities.AsNoTracking().ProjectTo<FilmResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
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