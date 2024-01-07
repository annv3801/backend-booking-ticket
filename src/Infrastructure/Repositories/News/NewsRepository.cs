using Application.DataTransferObjects.News.Requests;
using Application.DataTransferObjects.News.Responses;
using Application.Interface;
using Application.Repositories.News;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.News;

public class NewsRepository : Repository<NewsEntity, ApplicationDbContext>, INewsRepository
{
    private readonly DbSet<NewsEntity> _newsEntities;
    private readonly IMapper _mapper;

    public NewsRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _newsEntities = applicationDbContext.Set<NewsEntity>();
    }

    public async Task<OffsetPaginationResponse<NewsResponse>> GetListNewsAsync(ViewNewsRequest request, CancellationToken cancellationToken)
    {
        var query = _newsEntities.Where(x => !x.Deleted && x.GroupEntityId == request.GroupId && x.GroupEntity.Type == "News").OrderBy(x => x.Title.ToLower()).Select(x => new NewsResponse()
            {
                Title = x.Title,
                Status = x.Status,
                Id = x.Id,
                Description = x.Description,
                GroupEntityId = x.GroupEntityId,
                GroupEntity = x.GroupEntity,
                Image = x.Image
            });
        
        var response = await query.PaginateAsync<NewsEntity,NewsResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<NewsResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<NewsResponse?> GetNewsByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _newsEntities.AsNoTracking().ProjectTo<NewsResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<NewsEntity?> GetNewsEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _newsEntities.AsNoTracking().Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsDuplicatedNewsByNameAndIdAsync(string name, long id, CancellationToken cancellationToken)
    {
        return await _newsEntities.AsNoTracking().AnyAsync(x => x.Title == name && x.Id != id && !x.Deleted, cancellationToken);
    }

    public async Task<bool> IsDuplicatedNewsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _newsEntities.AsNoTracking().AnyAsync(x => x.Title == name && !x.Deleted, cancellationToken);
    }
    
}