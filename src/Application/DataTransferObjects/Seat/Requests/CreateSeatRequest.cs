// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Seat.Requests;

public class CreateSeatRequest
{
    public long SchedulerId { get; set; }
    public long RoomSeatId { get; set; }
    public long TicketId { get; set; }
}