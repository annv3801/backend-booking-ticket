using Application.DataTransferObjects.RoomSeat.Requests;
using Application.DataTransferObjects.RoomSeat.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.RoomSeat;

public interface IRoomSeatManagementService
{
    Task<RequestResult<bool>> CreateRoomSeatAsync(CreateRoomSeatRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateRoomSeatAsync(UpdateRoomSeatRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteRoomSeatAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<RoomSeatResponse>> GetRoomSeatAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<RoomSeatResponse>>> GetListRoomSeatsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<RequestResult<ICollection<RoomSeatResponse>>> GetListRoomSeatsByRoomAsync(long roomId, CancellationToken cancellationToken);
}