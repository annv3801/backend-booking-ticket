using Domain.Common;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class Booking : AuditableEntity
{
    public Guid Id { get; set; }
    public string BookingId { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    public double Total { get; set; }
    public double TotalBeforeDiscount { get; set; }
    public double Discount { get; set; }
    public string? CouponId { get; set; }
    public int PaymentMethod { get; set; }
    public int Status { get; set; }
    public int IsReceived { get; set; }
}