using Application.Common;
using Application.DataTransferObjects.Coupon.Responses;
using AutoMapper;

namespace Infrastructure.Profiles.Coupon;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<Domain.Entities.Coupon, ViewCouponResponse>().ReverseMap();
        CreateMap<Domain.Entities.Coupon, CouponResult>().ReverseMap();
    }
}