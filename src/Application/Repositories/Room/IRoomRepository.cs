using Application.DataTransferObjects.Room.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Room;
public interface IRoomRepository : IRepository<RoomEntity>
{
    Task<OffsetPaginationResponse<RoomResponse>> GetListRoomsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<RoomResponse?> GetRoomByIdAsync(long id, CancellationToken cancellationToken);
    Task<RoomEntity?> GetRoomEntityByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedRoomByNameAndIdAsync(string name, long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedRoomByNameAsync(string name, CancellationToken cancellationToken);
    
}
