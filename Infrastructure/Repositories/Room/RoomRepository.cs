using Application.DataTransferObjects.Room.Requests;
using Application.Repositories.Room;
using AutoMapper;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Room;

public class RoomRepository : Repository<Domain.Entities.Room>, IRoomRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public RoomRepository(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<Domain.Entities.Room?> GetRoomByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Rooms.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Room>> GetListRoomsAsync(ViewListRoomsRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.Rooms
            .AsSplitQuery()
            .AsQueryable();
    }
    
}