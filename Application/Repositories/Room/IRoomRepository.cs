using Application.Common.Interfaces;
using Application.DataTransferObjects.Room.Requests;

namespace Application.Repositories.Room;

public interface IRoomRepository : IRepository<Domain.Entities.Room>
{
    Task<IQueryable<Domain.Entities.Room>> GetListRoomsAsync(ViewListRoomsRequest request, CancellationToken cancellationToken);
    Task<Domain.Entities.Room?> GetRoomByIdAsync(Guid id, CancellationToken cancellationToken);
}