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
    private readonly IMapper _mapper;

    public TheaterRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _theaterEntities = applicationDbContext.Set<TheaterEntity>();
    }

    public async Task<OffsetPaginationResponse<TheaterResponse>> GetListTheatersAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
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
        
        var response = await query.PaginateAsync<TheaterEntity,TheaterResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<TheaterResponse>()
        {
            Data = response.Data,
            PageSize = response.CurrentPage,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<TheaterResponse?> GetTheaterByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _theaterEntities.AsNoTracking().ProjectTo<TheaterResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
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