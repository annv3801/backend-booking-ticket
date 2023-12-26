using Domain.Entities;

namespace Application.DataTransferObjects.RoomSeat.Responses;

public class RoomSeatByRoomResponse 
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public RoomEntity Room { get; set; }
    public List<string> SeatName { get; set; }
}
