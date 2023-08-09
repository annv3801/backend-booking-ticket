using Application.DataTransferObjects.Coupon.Requests;
using Application.DataTransferObjects.Slider.Requests;
using Application.Repositories.Coupon;
using Application.Repositories.Slider;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Slider;

public class CouponRepository : Repository<Domain.Entities.Coupon>, ICouponRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    
    public CouponRepository(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<Domain.Entities.Coupon?> GetCouponByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Coupons.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Coupon?> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Coupons.AsSplitQuery().FirstOrDefaultAsync(r => r.Code == code, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Coupon>> GetListCouponAsync(ViewListCouponsRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return _applicationDbContext.Coupons
            .AsSplitQuery()
            .AsQueryable();
    }
    
}