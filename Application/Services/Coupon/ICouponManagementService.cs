using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Coupon.Requests;
using Application.DataTransferObjects.Coupon.Responses;
using Application.DataTransferObjects.Pagination.Responses;

namespace Application.Services.Coupon;
public interface ICouponManagementService
{
    Task<Result<CouponResult>> CreateCouponAsync(CreateCouponRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewCouponResponse>> ViewCouponAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<CouponResult>> DeleteCouponAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<CouponResult>> UpdateCouponAsync(Guid id, UpdateCouponRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewCouponResponse>>> ViewListCouponAsync(ViewListCouponsRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewCouponResponse>> ViewCouponByCodeAsync(string code, CancellationToken cancellationToken = default(CancellationToken));

}
