using Application.Common.Interfaces;
using Application.DataTransferObjects.Room.Requests;
using Application.Services.Room;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class RoomController : Controller
{
    private readonly IRoomManagementService _roomManagementService;

    public RoomController(IRoomManagementService roomManagementService)
    {
        _roomManagementService = roomManagementService;
    }
    
    [HttpPost]
    [Route("create-room")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.CreateRoomAsync(request, cancellationToken);
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
    [Route("delete-room/{id}")]
    public async Task<IActionResult> DeleteRoomAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.DeleteRoomAsync(id, cancellationToken);
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
    [Route("update-room/{id}")]
    public async Task<IActionResult> UpdateRoomAsync(Guid id, UpdateRoomRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.UpdateRoomAsync(id, request, cancellationToken);
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
    [Route("view-room/{id}")]
    public async Task<IActionResult> ViewRoomAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.ViewRoomAsync(id, cancellationToken);
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
    [Route("view-list-room")]
    // [Cached]
    public async Task<IActionResult>? ViewListRoomsAsync([FromQuery] ViewListRoomsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _roomManagementService.ViewListRoomsAsync(request, cancellationToken);
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