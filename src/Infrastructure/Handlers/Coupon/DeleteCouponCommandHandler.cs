using Application.Commands.Coupon;
using Application.Handlers.Coupon;
using Application.Repositories.Coupon;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Coupon;

public class DeleteCouponCommandHandler : IDeleteCouponCommandHandler
{
    private readonly ICouponRepository _couponRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public DeleteCouponCommandHandler(ICouponRepository couponRepository, ApplicationDbContext applicationDbContext)
    {
        _couponRepository = couponRepository;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(DeleteCouponCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _couponRepository.Update(command.Entity);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}