using Application.DataTransferObjects.Room.Responses;
using Application.Interface;
using Application.Repositories.Room;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Room;

public class RoomRepository : Repository<RoomEntity, ApplicationDbContext>, IRoomRepository
{
    private readonly DbSet<RoomEntity> _roomEntities;
    private readonly IMapper _mapper;

    public RoomRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _roomEntities = applicationDbContext.Set<RoomEntity>();
    }

    public async Task<OffsetPaginationResponse<RoomResponse>> GetListRoomsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _roomEntities.Where(x => !x.Deleted).OrderBy(x => x.Name.ToLower()).Include(x => x.Theater).Select(x => new RoomResponse()
            {
                Id = x.Id,
                Name = x.Name,
                TheaterId = x.TheaterId,
                Status = x.Status,
                Theater = x.Theater,
                CreatedTime = x.CreatedTime
            });
        
        var response = await query.PaginateAsync<RoomEntity,RoomResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<RoomResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<RoomResponse?> GetRoomByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _roomEntities.AsNoTracking().ProjectTo<RoomResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<RoomEntity?> GetRoomEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _roomEntities.AsNoTracking().Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsDuplicatedRoomByNameAndIdAsync(string name, long id, CancellationToken cancellationToken)
    {
        return await _roomEntities.AsNoTracking().AnyAsync(x => x.Name == name && x.Id != id && !x.Deleted, cancellationToken);
    }

    public async Task<bool> IsDuplicatedRoomByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _roomEntities.AsNoTracking().AnyAsync(x => x.Name == name && !x.Deleted, cancellationToken);
    }
    
}