// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.RoomSeat.Requests;

public class CreateRoomSeatRequest
{
    public List<RoomSeatRequest> RoomSeat { get; set; }
}
public class RoomSeatRequest
{
    public string Name { get; set; }
    public long RoomId { get; set; }
}