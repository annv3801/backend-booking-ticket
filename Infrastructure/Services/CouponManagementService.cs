using Application.Commands.Coupon;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Coupon.Requests;
using Application.DataTransferObjects.Coupon.Responses;
using Application.Repositories.Coupon;
using Application.Services.Coupon;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Infrastructure.Services;

public class CouponManagementService : ICouponManagementService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICouponRepository _couponRepository;
    private readonly IStringLocalizationService _localizationService;
    public readonly ICurrentAccountService CurrentAccountService;
    private readonly IPaginationService _paginationService;


    public CouponManagementService(IMediator mediator, IMapper mapper, ICouponRepository couponRepository, IStringLocalizationService localizationService, ICurrentAccountService currentAccountService, IPaginationService paginationService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _couponRepository = couponRepository;
        _localizationService = localizationService;
        CurrentAccountService = currentAccountService;
        _paginationService = paginationService;
    }
    
    public async Task<Result<CouponResult>> CreateCouponAsync(CreateCouponRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var newField = new Coupon()
            {
                Id = new Guid(),
                Name = request.Name,
                Value = request.Value,
                Code = request.Code,
                Quantity = request.Quantity,
                RemainingQuantity = request.Quantity,
                EffectiveStartDate = request.EffectiveStartDate,
                EffectiveEndDate = request.EffectiveEndDate,
                Status = request.Status
            };

            var result = await _mediator.Send(new CreateCouponCommand(newField), cancellationToken);
            return result > 0 ? Result<CouponResult>.Succeed(_mapper.Map<CouponResult>(newField)) : Result<CouponResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewCouponResponse>> ViewCouponAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _couponRepository.GetCouponByIdAsync(id, cancellationToken);
            if (result == null)
                return Result<ViewCouponResponse>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            var couponResult = _mapper.Map<ViewCouponResponse>(result);

            return Result<ViewCouponResponse>.Succeed(couponResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<CouponResult>> DeleteCouponAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check for existence
            var couponToDelete = await _couponRepository.GetCouponByIdAsync(id, cancellationToken);
            if (couponToDelete == null)
                return Result<CouponResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            couponToDelete.Status = 0;
            couponToDelete.LastModifiedById = CurrentAccountService.Id;
            couponToDelete.LastModified = DateTime.Now;

            var resultDeleteTicket = await _mediator.Send(new DeleteCouponCommand(couponToDelete), cancellationToken);
            return resultDeleteTicket <= 0 ? Result<CouponResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<CouponResult>.Succeed(_mapper.Map<CouponResult>(couponToDelete));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<CouponResult>> UpdateCouponAsync(Guid id, UpdateCouponRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find Coupon
            var existedCoupon = await _couponRepository.GetCouponByIdAsync(id, cancellationToken);
            if (existedCoupon == null)
                return Result<CouponResult>.Fail(LocalizationString.Category.NotFoundCategory.ToErrors(_localizationService));

            existedCoupon.Name = request.Name;
            existedCoupon.Code = request.Code;
            existedCoupon.Quantity = request.Quantity;
            existedCoupon.RemainingQuantity = request.Quantity;
            existedCoupon.Value = request.Value;
            existedCoupon.EffectiveStartDate = request.EffectiveStartDate;
            existedCoupon.EffectiveEndDate = request.EffectiveEndDate;
            existedCoupon.Status = request.Status;
            existedCoupon.LastModified = DateTime.Now;
            existedCoupon.LastModifiedById = CurrentAccountService.Id;

            var resultUpdateCoupon = await _mediator.Send(new UpdateCouponCommand(existedCoupon), cancellationToken);
            return resultUpdateCoupon <= 0 ? Result<CouponResult>.Fail(LocalizationString.Common.SaveFailed.ToErrors(_localizationService)) : Result<CouponResult>.Succeed(_mapper.Map<CouponResult>(existedCoupon));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewCouponResponse>>> ViewListCouponAsync(ViewListCouponsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var source = await _couponRepository.GetListCouponAsync(request, cancellationToken);
            var result = await _paginationService.PaginateAsync(source, request.Page, request.OrderBy, request.OrderByDesc, request.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewCouponResponse>>.Fail(
                    _localizationService[LocalizationString.Category.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewCouponResponse>>.Succeed(new PaginationBaseResponse<ViewCouponResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewCouponResponse()
                {
                    Id = a.Id,
                    Name = a.Name, 
                    Code = a.Code,
                    EffectiveStartDate = a.EffectiveStartDate,
                    EffectiveEndDate = a.EffectiveEndDate,
                    Value = a.Value,
                    Quantity = a.Quantity,
                    RemainingQuantity = a.RemainingQuantity,
                    Status = a.Status
                }).ToList()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ViewCouponResponse>> ViewCouponByCodeAsync(string code, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var coupon = await _couponRepository.GetCouponByCodeAsync(code, cancellationToken);
            if (coupon == null)
                return Result<ViewCouponResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewCouponResponse>.Succeed(new ViewCouponResponse()
            {
                Id = coupon.Id,
                Name = coupon.Name, 
                Code = coupon.Code,
                EffectiveStartDate = coupon.EffectiveStartDate,
                EffectiveEndDate = coupon.EffectiveEndDate,
                Value = coupon.Value,
                Quantity = coupon.Quantity,
                RemainingQuantity = coupon.RemainingQuantity,
                Status = coupon.Status
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}