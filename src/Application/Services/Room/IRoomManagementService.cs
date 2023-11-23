using Application.DataTransferObjects.Room.Requests;
using Application.DataTransferObjects.Room.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Room;

public interface IRoomManagementService
{
    Task<RequestResult<bool>> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateRoomAsync(UpdateRoomRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteRoomAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<RoomResponse>> GetRoomAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<RoomResponse>>> GetListRoomsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
}