namespace Application.DataTransferObjects.Coupon.Requests;

public class CreateCouponRequest
{
    public string Name { get; set; }
    public string Code { get; set; }
    public DateTime EffectiveStartDate { get; set; }
    public DateTime EffectiveEndDate { get; set; }
    public double Value { get; set; }
    public int Quantity { get; set; }
    public int Status { get; set; }
}