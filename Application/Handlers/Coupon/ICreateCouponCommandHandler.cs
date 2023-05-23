using Application.Commands.Coupon;
using MediatR;

namespace Application.Handlers.Coupon;

public interface ICreateCouponCommandHandler: IRequestHandler<CreateCouponCommand, int>
{
    
}