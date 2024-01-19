using Application.DataTransferObjects.Category.Responses;
using Application.Interface;
using Application.Repositories.Category;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Category;

public class CategoryRepository : Repository<CategoryEntity, ApplicationDbContext>, ICategoryRepository
{
    private readonly DbSet<CategoryEntity> _categoryEntities;
    private readonly IMapper _mapper;

    public CategoryRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _categoryEntities = applicationDbContext.Set<CategoryEntity>();
    }

    public async Task<OffsetPaginationResponse<CategoryResponse>> GetListCategoriesAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _categoryEntities.Where(x => !x.Deleted).OrderBy(x => x.Name.ToLower()).Select(x => new CategoryResponse()
            {
                Name = x.Name,
                Status = x.Status,
                Id = x.Id,
                CreatedTime = x.CreatedTime,
            });
        
        var response = await query.PaginateAsync<CategoryEntity,CategoryResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<CategoryResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<CategoryResponse?> GetCategoryByIdAsync(long id, CancellationToken cancellationToken)
    {
        
        return await _categoryEntities.AsNoTracking().ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<CategoryEntity?> GetCategoryEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _categoryEntities.AsNoTracking().Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsDuplicatedCategoryByNameAndIdAsync(string name, long id, CancellationToken cancellationToken)
    {
        return await _categoryEntities.AsNoTracking().AnyAsync(x => x.Name == name && x.Id != id && !x.Deleted, cancellationToken);
    }

    public async Task<bool> IsDuplicatedCategoryByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _categoryEntities.AsNoTracking().AnyAsync(x => x.Name == name && !x.Deleted, cancellationToken);
    }
    
}