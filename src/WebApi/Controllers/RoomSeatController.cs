using Application.DataTransferObjects.RoomSeat.Requests;
using Application.DataTransferObjects.RoomSeat.Responses;
using Application.Services.RoomSeat;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching RoomSeat, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("RoomSeat")]
public class RoomSeatController : ControllerBase
{
    private readonly IRoomSeatManagementService _roomSeatManagementService;
    private readonly ILoggerService _loggerService;

    public RoomSeatController(IRoomSeatManagementService roomSeatManagementService, ILoggerService loggerService)
    {
        _roomSeatManagementService = roomSeatManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new RoomSeat
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-RoomSeat")]
    public async Task<RequestResult<bool>?> CreateRoomSeatAsync(CreateRoomSeatRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomSeatManagementService.CreateRoomSeatAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateRoomSeatAsync));
            throw;
        }
    }

    /// <summary>
    /// Update RoomSeat
    /// </summary>
    /// <param name="updateRoomSeatRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-RoomSeat")]
    public async Task<RequestResult<bool>?> UpdateRoomSeatAsync(UpdateRoomSeatRequest updateRoomSeatRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomSeatManagementService.UpdateRoomSeatAsync(updateRoomSeatRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateRoomSeatAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Delete RoomSeat with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-RoomSeat/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteRoomSeatAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomSeatManagementService.DeleteRoomSeatAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteRoomSeatAsync));
            throw;
        }
    }

    /// <summary>
    /// Get RoomSeat with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-RoomSeat/{id:long}")]
    public async Task<RequestResult<RoomSeatResponse>?> ViewRoomSeatAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomSeatManagementService.GetRoomSeatAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewRoomSeatAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list RoomSeat with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-List-RoomSeats")]
    public async Task<RequestResult<OffsetPaginationResponse<RoomSeatResponse>>> ViewListRoomSeatsAsync([FromQuery] OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomSeatManagementService.GetListRoomSeatsAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListRoomSeatsAsync));
            throw;
        }
    }
}