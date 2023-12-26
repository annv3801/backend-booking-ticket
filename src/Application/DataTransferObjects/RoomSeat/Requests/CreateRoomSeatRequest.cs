// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.RoomSeat.Requests;

public class CreateRoomSeatRequest
{
    public string Name { get; set; }
    public long RoomId { get; set; }
}