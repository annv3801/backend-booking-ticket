using Application.Commands.Coupon;
using Application.Commands.FilmSchedules;
using Application.Handlers.Coupon;
using Application.Handlers.FilmSchedules;
using Application.Repositories.Coupon;
using Application.Repositories.FilmSchedules;
using Infrastructure.Databases;

namespace Infrastructure.Handlers.FilmSchedules;

public class CreateCouponCommandHandler : ICreateCouponCommandHandler
{
    private readonly ICouponRepository _couponRepository;
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateCouponCommandHandler(ICouponRepository couponRepository, ApplicationDbContext applicationDbContext)
    {
        _couponRepository = couponRepository;
        _applicationDbContext = applicationDbContext;
    }


    public async Task<int> Handle(CreateCouponCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _couponRepository.AddAsync(command.Entity, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}