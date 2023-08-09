using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Coupon.Responses;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using MediatR;

namespace Application.Queries.Coupon;
[ExcludeFromCodeCoverage]
public class ViewListCouponsQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewCouponResponse>>>
{
}
