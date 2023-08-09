using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Coupon.Responses;
using MediatR;

namespace Application.Queries.Coupon;
[ExcludeFromCodeCoverage]
public class ViewCouponByIdQuery : IRequest<Result<ViewCouponResponse>>
{
    public ViewCouponByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
