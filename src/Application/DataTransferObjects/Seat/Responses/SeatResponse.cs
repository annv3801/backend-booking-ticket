using Domain.Entities;

namespace Application.DataTransferObjects.Seat.Responses;

public class SeatResponse 
{
    public long Id { get; set; }
    public long SchedulerId { get; set; }
    public SchedulerEntity Scheduler { get; set; }
    public long RoomsSeatId { get; set; }
    public RoomSeatEntity RoomSeat { get; set; }
    public string Status { get; set; }
}
