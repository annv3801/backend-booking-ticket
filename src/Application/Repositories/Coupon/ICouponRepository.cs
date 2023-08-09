using Application.Common.Interfaces;
using Application.DataTransferObjects.Coupon.Requests;

namespace Application.Repositories.Coupon;

public interface ICouponRepository : IRepository<Domain.Entities.Coupon>
{
    Task<IQueryable<Domain.Entities.Coupon>> GetListCouponAsync(ViewListCouponsRequest request, CancellationToken cancellationToken);
    Task<Domain.Entities.Coupon?> GetCouponByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Domain.Entities.Coupon?> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default(CancellationToken));

}