using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Coupon.Requests;
using MediatR;

namespace Application.Commands.Coupon;
[ExcludeFromCodeCoverage]
public class UpdateCouponCommand : UpdateCouponRequest, IRequest<int>
{
    public UpdateCouponCommand(Domain.Entities.Coupon entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Coupon Entity { get; set; }
}
