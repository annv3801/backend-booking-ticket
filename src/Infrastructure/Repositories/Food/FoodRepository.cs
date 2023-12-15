using Application.DataTransferObjects.Food.Requests;
using Application.DataTransferObjects.Food.Responses;
using Application.Interface;
using Application.Repositories.Food;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Food;

public class FoodRepository : Repository<FoodEntity, ApplicationDbContext>, IFoodRepository
{
    private readonly DbSet<FoodEntity> _foodEntities;
    private readonly IMapper _mapper;

    public FoodRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _foodEntities = applicationDbContext.Set<FoodEntity>();
    }

    public async Task<OffsetPaginationResponse<FoodResponse>> GetListFoodsAsync(ViewFoodRequest request, CancellationToken cancellationToken)
    {
        var query = _foodEntities.Where(x => !x.Deleted && x.GroupEntityId == request.GroupId && x.GroupEntity.Type == "FOOD").OrderBy(x => x.Title.ToLower()).Select(x => new FoodResponse()
            {
                Title = x.Title,
                Status = x.Status,
                Id = x.Id,
                Description = x.Description,
                Price = x.Price,
                GroupEntityId = x.GroupEntityId,
                GroupEntity = x.GroupEntity,
                Image = x.Image
            });
        
        var response = await query.PaginateAsync<FoodEntity,FoodResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<FoodResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<FoodResponse?> GetFoodByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _foodEntities.AsNoTracking().ProjectTo<FoodResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<FoodEntity?> GetFoodEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _foodEntities.AsNoTracking().Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsDuplicatedFoodByNameAndIdAsync(string name, long id, CancellationToken cancellationToken)
    {
        return await _foodEntities.AsNoTracking().AnyAsync(x => x.Title == name && x.Id != id && !x.Deleted, cancellationToken);
    }

    public async Task<bool> IsDuplicatedFoodByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _foodEntities.AsNoTracking().AnyAsync(x => x.Title == name && !x.Deleted, cancellationToken);
    }
    
}