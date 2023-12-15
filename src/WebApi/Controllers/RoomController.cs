using Application.DataTransferObjects.Room.Requests;
using Application.DataTransferObjects.Room.Responses;
using Application.Services.Room;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching Room, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Room")]
public class RoomController : ControllerBase
{
    private readonly IRoomManagementService _roomManagementService;
    private readonly ILoggerService _loggerService;

    public RoomController(IRoomManagementService roomManagementService, ILoggerService loggerService)
    {
        _roomManagementService = roomManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new Room
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Room")]
    public async Task<RequestResult<bool>?> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.CreateRoomAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateRoomAsync));
            throw;
        }
    }

    /// <summary>
    /// Update Room
    /// </summary>
    /// <param name="updateRoomRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-Room")]
    public async Task<RequestResult<bool>?> UpdateRoomAsync(UpdateRoomRequest updateRoomRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.UpdateRoomAsync(updateRoomRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateRoomAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Delete Room with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-Room/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteRoomAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.DeleteRoomAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteRoomAsync));
            throw;
        }
    }

    /// <summary>
    /// Get Room with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-Room/{id:long}")]
    public async Task<RequestResult<RoomResponse>?> ViewRoomAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.GetRoomAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewRoomAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Room with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Rooms")]
    public async Task<RequestResult<OffsetPaginationResponse<RoomResponse>>> ViewListRoomsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.GetListRoomsAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListRoomsAsync));
            throw;
        }
    }
}