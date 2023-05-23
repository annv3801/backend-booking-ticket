using Domain.Common;

namespace Domain.Entities;

public class Coupon : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public DateTime EffectiveStartDate { get; set; }
    public DateTime EffectiveEndDate { get; set; }
    public double Value { get; set; }
    public int Quantity { get; set; }
    public int RemainingQuantity { get; set; }
    public int Status { get; set; }
}