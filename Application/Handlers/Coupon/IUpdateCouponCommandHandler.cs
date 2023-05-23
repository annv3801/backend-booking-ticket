using Application.Commands.Coupon;
using MediatR;

namespace Application.Handlers.Coupon;

public interface IUpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, int>
{
}