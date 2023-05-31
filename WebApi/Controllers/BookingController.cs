using Application.Commands.Booking;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.VnPay;
using Application.Repositories.Booking;
using Application.Services.Booking;
using AutoMapper;
using Infrastructure.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IVnPayService _vnPayService;
    private readonly IBookingManagementService _bookingManagementService;
    public BookingController(IMediator mediator, IMapper mapper, IVnPayService vnPayService, IBookingManagementService bookingManagementService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _vnPayService = vnPayService;
        _bookingManagementService = bookingManagementService;
    }
    
    [HttpPost]
    // [Permissions(Permissions = new[] {Constants.Permissions.SysAdmin, Constants.Permissions.TenantAdmin})]
    [Route("booking")]
    [Produces("application/json")]

    public async Task<IActionResult> BookingAsync(BookingRequest bookingRequest, CancellationToken cancellationToken)
    {
        try
        {
            var createCategoryCommand = _mapper.Map<BookingCommand>(bookingRequest);
            var result = await _mediator.Send(createCategoryCommand, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: new {result.Data}));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPut]
    [Route("update-received-booking/{id}")]
    public async Task<IActionResult> UpdateReceicedBookingAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _bookingManagementService.UpdateReceivedBookingAsync(id, cancellationToken);
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
    
    [HttpPost]
    [Route("payment-vnpay")]
    public async Task<IActionResult> CreatePaymentUrl(PaymentInformationModel model)
    {
        var url = _vnPayService.CreatePaymentUrl(model);
        return Ok(url);
    }
    
    [HttpGet]
    [Route("PaymentVnPay/Callback")]
    public async Task<IActionResult> PaymentCallback()
    {
        var response = _vnPayService.PaymentExecute(Request.Query);
        var result = "";
        if (response.VnPayResponseCode == "00")
        {
            result = "http://localhost:3000/payment-success";
        }
        return Redirect(result);
    }
    
    [HttpGet]
    [Route("view-list-booking-by-user")]
    // [Cached]
    public async Task<IActionResult>? ViewListCategoriesAsync([FromQuery] ViewListBookingByUserRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _bookingManagementService.ViewListBookingsByUserAsync(request, cancellationToken);
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
    [Route("view-list-booking-by-admin")]
    // [Cached]
    public async Task<IActionResult>? ViewListCategoriesByAdminAsync([FromQuery] ViewListBookingByAdminRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _bookingManagementService.ViewListBookingsByAdminAsync(request, cancellationToken);
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