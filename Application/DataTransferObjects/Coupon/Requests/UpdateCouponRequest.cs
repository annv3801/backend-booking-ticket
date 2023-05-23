using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Coupon.Requests;
[ExcludeFromCodeCoverage]
public class UpdateCouponRequest
{
    public string Name { get; set; }
    public string Code { get; set; }
    public DateTime EffectiveStartDate { get; set; }
    public DateTime EffectiveEndDate { get; set; }
    public double Value { get; set; }
    public int Quantity { get; set; }
    public int Status { get; set; }
}
