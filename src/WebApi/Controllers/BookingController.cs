using Application.DataTransferObjects.Booking.Requests;
using Application.DataTransferObjects.Booking.Responses;
using Application.Services.Booking;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Bookings. Supports creating, updating, 
/// deleting and fetching Booking, and listing Bookings with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Booking")]
public class BookingController : ControllerBase
{
    private readonly IBookingManagementService _bookingManagementService;
    private readonly ILoggerService _loggerService;

    public BookingController(IBookingManagementService bookingManagementService, ILoggerService loggerService)
    {
        _bookingManagementService = bookingManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new Booking
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Booking")]
    public async Task<RequestResult<bool>?> CreateBookingAsync(CreateBookingRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _bookingManagementService.CreateBookingAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateBookingAsync));
            throw;
        }
    }
    
    /// <summary>
    /// Get Booking with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-Booking/{id:long}")]
    public async Task<RequestResult<BookingResponse>?> ViewBookingAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _bookingManagementService.GetBookingAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewBookingAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Booking with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-List-Bookings")]
    public async Task<RequestResult<OffsetPaginationResponse<BookingResponse>>> ViewListBookingsAsync([FromQuery] ViewListBookingsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _bookingManagementService.GetListBookingsAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListBookingsAsync));
            throw;
        }
    }
}