using Application.DataTransferObjects.Coupon.Requests;
using Application.Services.Coupon;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class CouponController : Controller
{
    private readonly ICouponManagementService _couponManagementService;

    public CouponController(ICouponManagementService couponManagementService)
    {
        _couponManagementService = couponManagementService;
    }
    
    [HttpPost]
    [Route("create-coupon")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateCouponAsync(CreateCouponRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _couponManagementService.CreateCouponAsync(request, cancellationToken);
            if (!result.Succeeded) return Accepted(result);
            if (result.Data != null)
                return Ok(result);
            return Accepted(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpDelete]
    [Route("delete-coupon/{id}")]
    public async Task<IActionResult> DeleteCouponAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _couponManagementService.DeleteCouponAsync(id, cancellationToken);
            if (!result.Succeeded) return Accepted(result);
            if (result.Data != null)
                return Ok(result);
            return Accepted(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPut]
    [Route("update-coupon/{id}")]
    public async Task<IActionResult> UpdateCouponAsync(Guid id, UpdateCouponRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _couponManagementService.UpdateCouponAsync(id, request, cancellationToken);
            if (!result.Succeeded) return Accepted(result);
            if (result.Data != null)
                return Ok(result);
            return Accepted(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    [Route("view-coupon/{id}")]
    public async Task<IActionResult> ViewCouponAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _couponManagementService.ViewCouponAsync(id, cancellationToken);
            if (!result.Succeeded) return Accepted(result);
            if (result.Data != null)
                return Ok(result);
            return Accepted(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet]
    [Route("view-list-coupon")]
    // [Cached]
    public async Task<IActionResult>? ViewListCouponsAsync([FromQuery] ViewListCouponsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _couponManagementService.ViewListCouponAsync(request, cancellationToken);
            if (!result.Succeeded) return Accepted(result);
            if (result.Data != null)
                return Ok(result);
            return Accepted(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}