using Application.DataTransferObjects.Seat.Responses;
using Domain.Entities;

namespace Application.DataTransferObjects.Booking.Responses;

public class BookingResponse
{
    public long Id { get; set; }
    public double Total { get; set; }
    public double TotalBeforeDiscount { get; set; }
    public double Discount { get; set; }
    public long CouponId { get; set; }
    public int PaymentMethod { get; set; }
    public int Status { get; set; }
    public int IsReceived { get; set; }
    public long AccountId { get; set; }
    public Domain.Entities.Identity.Account Account { get; set; }
    public List<SeatResponse> Seats { get; set; }
    public List<FoodRequest> Foods { get; set; }
}

public class FoodBooking
{
    public long FoodId { get; set; }
    public string FoodName { get; set; }
    public long Quantity { get; set; }
}
