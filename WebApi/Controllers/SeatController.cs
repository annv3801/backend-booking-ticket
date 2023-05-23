using Application.DataTransferObjects.Seat.Requests;
using Application.Services.Seat;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class SeatController : Controller
{
    private readonly ISeatManagementService _seatManagementService;

    public SeatController(ISeatManagementService seatManagementService)
    {
        _seatManagementService = seatManagementService;
    }
    
    [HttpPost]
    [Route("create-seat")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateSeatAsync(CreateSeatRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _seatManagementService.CreateSeatAsync(request, cancellationToken);
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
    [Route("delete-seat/{id}")]
    public async Task<IActionResult> DeleteSeatAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _seatManagementService.DeleteSeatAsync(id, cancellationToken);
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
    [Route("view-seat/{id}")]
    public async Task<IActionResult> ViewSeatAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _seatManagementService.ViewSeatAsync(id, cancellationToken);
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
    [Route("view-list-seat")]
    // [Cached]
    public async Task<IActionResult>? ViewListSeatsAsync([FromQuery] ViewListSeatsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _seatManagementService.ViewListSeatsAsync(request, cancellationToken);
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
    [Route("view-list-seat-by-schedule")]
    // [Cached]
    public async Task<IActionResult>? ViewListSeatsByScheduleAsync([FromQuery] ViewListSeatsByScheduleRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _seatManagementService.ViewListSeatsByScheduleAsync(request, cancellationToken);
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