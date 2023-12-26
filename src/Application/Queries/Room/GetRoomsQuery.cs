using Application.DataTransferObjects.Room.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Room;

public class GetListRoomsQuery : IRequest<OffsetPaginationResponse<RoomResponse>>
{
    public OffsetPaginationRequest OffsetPaginationRequest { get; set; }
}