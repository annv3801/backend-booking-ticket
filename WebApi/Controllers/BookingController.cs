using Application.Commands.Booking;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.VnPay;
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
    public BookingController(IMediator mediator, IMapper mapper, IVnPayService vnPayService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _vnPayService = vnPayService;
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
    
    [HttpPost]
    [Route("payment-vnpay")]
    public async Task<IActionResult> CreatePaymentUrl(PaymentInformationModel model)
    {
        var url = _vnPayService.CreatePaymentUrl(model);
        return Ok(url);
    }
    
    [HttpGet]
    [Route("payment-vnpay-callback")]
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
}