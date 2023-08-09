using Application.Commands.Coupon;
using Application.Handlers.Coupon;
using Application.Repositories.Coupon;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.Coupon;

public class UpdateCouponCommandHandler : IUpdateCouponCommandHandler
{
    private readonly ICouponRepository _couponRepository;
    private readonly ApplicationDbContext _applicationDbContext;
    
    public UpdateCouponCommandHandler(ApplicationDbContext applicationDbContext, ICouponRepository couponRepository)
    {
        _applicationDbContext = applicationDbContext;
        _couponRepository = couponRepository;
    }

    public async Task<int> Handle(UpdateCouponCommand command, CancellationToken cancellationToken)
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