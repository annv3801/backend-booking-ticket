using Application.DataTransferObjects.Seat.Responses;

namespace Application.DataTransferObjects.Booking.Responses;

public class BookingResponse
{
    public long Id { get; set; }
    public long AccountId { get; set; }
    public Domain.Entities.Identity.Account Account { get; set; }
    public double Total { get; set; }
    public int IsReceived { get; set; }
    public List<SeatResponse> Seats { get; set; }
}