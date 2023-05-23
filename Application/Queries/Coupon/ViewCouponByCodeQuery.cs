using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Coupon.Responses;
using MediatR;

namespace Application.Queries.Coupon;
[ExcludeFromCodeCoverage]
public class ViewCouponByCodeQuery : IRequest<Result<ViewCouponResponse>>
{
    public string Code { get; set; }
}
