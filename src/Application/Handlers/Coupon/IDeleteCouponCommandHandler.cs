using Application.Commands.Coupon;
using MediatR;

namespace Application.Handlers.Coupon;

public interface IDeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, int>
{
}