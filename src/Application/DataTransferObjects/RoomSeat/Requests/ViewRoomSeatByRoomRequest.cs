using System.Diagnostics.CodeAnalysis;
using Domain.Common.Pagination.OffsetBased;

namespace Application.DataTransferObjects.RoomSeat.Requests;

[ExcludeFromCodeCoverage]
public class ViewRoomSeatByRoomRequest
{
    public long RoomId { get; set; }
}