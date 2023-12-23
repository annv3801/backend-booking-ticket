using Application.DataTransferObjects.RoomSeat.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.RoomSeat;
public interface IRoomSeatRepository : IRepository<RoomSeatEntity>
{
    Task<OffsetPaginationResponse<RoomSeatResponse>> GetListRoomSeatsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<RoomSeatResponse?> GetRoomSeatByIdAsync(long id, CancellationToken cancellationToken);
    Task<RoomSeatEntity?> GetRoomSeatEntityByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedRoomSeatByNameAndIdAsync(string name, long id, long roomId, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedRoomSeatByNameAsync(string name, long roomId, CancellationToken cancellationToken);
}
