using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Room.Requests;
using Application.DataTransferObjects.Room.Responses;

namespace Application.Services.Room;
public interface IRoomManagementService
{
    Task<Result<RoomResult>> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewRoomResponse>> ViewRoomAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<RoomResult>> DeleteRoomAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<RoomResult>> UpdateRoomAsync(Guid id, UpdateRoomRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewRoomResponse>>> ViewListRoomsAsync(ViewListRoomsRequest request, CancellationToken cancellationToken = default(CancellationToken));

}
