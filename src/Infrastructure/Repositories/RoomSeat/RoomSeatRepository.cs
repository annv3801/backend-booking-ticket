using Application.DataTransferObjects.RoomSeat.Responses;
using Application.Interface;
using Application.Repositories.RoomSeat;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Constants;
using Domain.Entities;
using Domain.Extensions;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.RoomSeat;

public class RoomSeatRepository : Repository<RoomSeatEntity, ApplicationDbContext>, IRoomSeatRepository
{
    private readonly DbSet<RoomSeatEntity> _roomSeatEntities;
    private readonly IMapper _mapper;

    public RoomSeatRepository(ApplicationDbContext applicationDbContext, IMapper mapper, ISnowflakeIdService snowflakeIdService) : base(applicationDbContext, snowflakeIdService)
    {
        _mapper = mapper;
        _roomSeatEntities = applicationDbContext.Set<RoomSeatEntity>();
    }

    public async Task<OffsetPaginationResponse<RoomSeatResponse>> GetListRoomSeatsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        var query = _roomSeatEntities.Where(x => !x.Deleted).OrderBy(x => x.Name.ToLower()).Select(x => new RoomSeatResponse()
            {
                Name = x.Name,
                Status = x.Status,
                Id = x.Id,
                Room = x.Room,
                RoomId = x.RoomId
            });
        
        var response = await query.PaginateAsync<RoomSeatEntity,RoomSeatResponse>(request, cancellationToken);
        return new OffsetPaginationResponse<RoomSeatResponse>()
        {
            Data = response.Data,
            PageSize = response.PageSize,
            Total = response.Total,
            CurrentPage = response.CurrentPage
        };
    }

    public async Task<RoomSeatResponse?> GetRoomSeatByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _roomSeatEntities.AsNoTracking().ProjectTo<RoomSeatResponse>(_mapper.ConfigurationProvider).Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<RoomSeatEntity?> GetRoomSeatEntityByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _roomSeatEntities.AsNoTracking().Where(x => x.Id == id && x.Status != EntityStatus.Deleted).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsDuplicatedRoomSeatByNameAndIdAsync(string name, long id, long roomId, CancellationToken cancellationToken)
    {
        return await _roomSeatEntities.AsNoTracking().AnyAsync(x => x.Name == name && x.Id != id && x.RoomId == roomId && !x.Deleted, cancellationToken);
    }

    public async Task<bool> IsDuplicatedRoomSeatByNameAsync(string name, long roomId, CancellationToken cancellationToken)
    {
        return await _roomSeatEntities.AsNoTracking().AnyAsync(x => x.Name == name && x.RoomId == roomId && !x.Deleted, cancellationToken);
    }
    
}