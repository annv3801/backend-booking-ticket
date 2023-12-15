using Domain.Common;
using Domain.Common.Entities;
using Domain.Constants;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class BookingDetailEntity : Entity<long>
{
    public long Id { get; set; }
    public long BookingId { get; set; }
    public BookingEntity Booking { get; set; }
    public long SeatId { get; set; }
    public SeatEntity Seat { get; set; }
    public List<FoodRequest> Foods { get; set; }
}