using Domain.Entities;

namespace Application.DataTransferObjects.Booking.Requests;

public class CreateBookingRequest
{
    public List<long> SeatId { get; set; }
    public double Total { get; set; }
    public long CouponId { get; set; }
    public double TotalBeforeDiscount { get; set; }
    public double Discount { get; set; }
    public int PaymentMethod { get; set; }
    public List<FoodRequest>? Foods { get; set; }
}
