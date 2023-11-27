using Application.DataTransferObjects.Seat.Requests;
using Application.DataTransferObjects.Seat.Responses;
using Application.Services.Seat;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching Seat, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Seat")]
public class SeatController : ControllerBase
{
    private readonly ISeatManagementService _seatManagementService;
    private readonly ILoggerService _loggerService;

    public SeatController(ISeatManagementService seatManagementService, ILoggerService loggerService)
    {
        _seatManagementService = seatManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new Seat
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Seat")]
    public async Task<RequestResult<bool>?> CreateSeatAsync(CreateSeatRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _seatManagementService.CreateSeatAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateSeatAsync));
            throw;
        }
    }

    /// <summary>
    /// Update Seat
    /// </summary>
    /// <param name="updateSeatRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-Seat")]
    public async Task<RequestResult<bool>?> UpdateSeatAsync(UpdateSeatRequest updateSeatRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _seatManagementService.UpdateSeatAsync(updateSeatRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateSeatAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Delete Seat with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-Seat/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteSeatAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _seatManagementService.DeleteSeatAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteSeatAsync));
            throw;
        }
    }

    /// <summary>
    /// Get Seat with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-Seat/{id:long}")]
    public async Task<RequestResult<SeatResponse>?> ViewSeatAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _seatManagementService.GetSeatAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewSeatAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Seat with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-List-Seats")]
    public async Task<RequestResult<OffsetPaginationResponse<SeatResponse>>> ViewListSeatsAsync([FromQuery] OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _seatManagementService.GetListSeatsAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListSeatsAsync));
            throw;
        }
    }
}