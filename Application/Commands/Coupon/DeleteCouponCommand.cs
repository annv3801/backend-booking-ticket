using MediatR;

namespace Application.Commands.Coupon;

public class DeleteCouponCommand : IRequest<int>
{
    public DeleteCouponCommand(Domain.Entities.Coupon entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Coupon Entity { get; set; }
}