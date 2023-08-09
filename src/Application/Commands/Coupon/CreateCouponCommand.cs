using Application.DataTransferObjects.Coupon.Requests;
using MediatR;

namespace Application.Commands.Coupon;

public class CreateCouponCommand : CreateCouponRequest, IRequest<int>
{
    public CreateCouponCommand(Domain.Entities.Coupon entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Coupon Entity { get; set; }
}