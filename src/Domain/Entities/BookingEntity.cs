using Domain.Common;
using Domain.Common.Entities;
using Domain.Constants;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class BookingEntity : Entity<long>
{
    public long Id { get; set; }
    public double Total { get; set; }
    public double TotalBeforeDiscount { get; set; }
    public double Discount { get; set; }
    public long CouponId { get; set; }
    public int PaymentMethod { get; set; }
    public string Status { get; set; }
    public int IsReceived { get; set; }
    public long AccountId { get; set; }
    public Account Account { get; set; }
}

public class FoodRequest
{
    public long FoodId { get; set; }
    public long Quantity { get; set; }
}