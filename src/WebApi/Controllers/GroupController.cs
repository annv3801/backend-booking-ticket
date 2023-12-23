using Application.DataTransferObjects.Group.Requests;
using Application.DataTransferObjects.Group.Responses;
using Application.Services.Group;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching Group, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Group")]
public class GroupController : ControllerBase
{
    private readonly IGroupManagementService _groupManagementService;
    private readonly ILoggerService _loggerService;

    public GroupController(IGroupManagementService groupManagementService, ILoggerService loggerService)
    {
        _groupManagementService = groupManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new Group
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Group")]
    public async Task<RequestResult<bool>?> CreateGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _groupManagementService.CreateGroupAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateGroupAsync));
            throw;
        }
    }

    /// <summary>
    /// Update Group
    /// </summary>
    /// <param name="updateGroupRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-Group")]
    public async Task<RequestResult<bool>?> UpdateGroupAsync(UpdateGroupRequest updateGroupRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _groupManagementService.UpdateGroupAsync(updateGroupRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateGroupAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Delete Group with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-Group/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteGroupAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _groupManagementService.DeleteGroupAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteGroupAsync));
            throw;
        }
    }

    /// <summary>
    /// Get Group with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-Group/{id:long}")]
    public async Task<RequestResult<GroupResponse>?> ViewGroupAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _groupManagementService.GetGroupAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewGroupAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Group with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Groups")]
    public async Task<RequestResult<OffsetPaginationResponse<GroupResponse>>> ViewListGroupsByTypeAsync(OffsetPaginationRequest request, string type, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _groupManagementService.GetListGroupsByTypeAsync(request, type, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListGroupsByTypeAsync));
            throw;
        }
    }
    /// <summary>
    /// Get list Group with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Groups-Not-Have-Type")]
    public async Task<RequestResult<OffsetPaginationResponse<GroupResponse>>> ViewListGroupsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _groupManagementService.GetListGroupsAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListGroupsAsync));
            throw;
        }
    }
}