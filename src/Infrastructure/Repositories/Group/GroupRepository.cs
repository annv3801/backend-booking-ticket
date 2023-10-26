using Application.DataTransferObjects.Group.Responses;
using Application.Interface;
using Application.Repositories.Group;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Group;

public class GroupRepository : Repository<GroupEntity, ApplicationDbContext>, IGroupRepository
{
    private readonly DbSet<GroupEntity> _groupEntities;
    private readonly IMapper _mapper;

    public GroupRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _groupEntities = applicationDbContext.Set<GroupEntity>();
    }

    public async Task<OffsetPaginationResponse<GroupResponse>> GetListGroupsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _groupEntities.OrderBy(x => x.Title.ToLower()).Select(x => new GroupResponse()
            {
                Title = x.Title,
                Status = x.Status,
                Id = x.Id,
            });
        
        var response = await query.PaginateAsync<GroupEntity,GroupResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<GroupResponse>()
        {
            Data = response.Data,
            PageSize = response.CurrentPage,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<GroupResponse?> GetGroupByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _groupEntities.AsNoTracking().ProjectTo<GroupResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<GroupEntity?> GetGroupEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _groupEntities.AsNoTracking().Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsDuplicatedGroupByNameAndIdAsync(string name, long id, CancellationToken cancellationToken)
    {
        return await _groupEntities.AsNoTracking().AnyAsync(x => x.Title == name && x.Id != id && !x.Deleted, cancellationToken);
    }

    public async Task<bool> IsDuplicatedGroupByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _groupEntities.AsNoTracking().AnyAsync(x => x.Title == name && !x.Deleted, cancellationToken);
    }
    
}