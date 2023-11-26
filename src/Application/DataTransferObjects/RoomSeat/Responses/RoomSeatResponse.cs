using Domain.Entities;

namespace Application.DataTransferObjects.RoomSeat.Responses;

public class RoomSeatResponse 
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string Status { get; set; }
    public long RoomId { get; set; }
    public RoomEntity Room { get; set; }
}
