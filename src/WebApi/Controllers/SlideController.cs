using Application.DataTransferObjects.Slide.Requests;
using Application.DataTransferObjects.Slide.Responses;
using Application.Services.Slide;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching Slide, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Slide")]
public class SlideController : ControllerBase
{
    private readonly ISlideManagementService _slideManagementService;
    private readonly ILoggerService _loggerService;

    public SlideController(ISlideManagementService slideManagementService, ILoggerService loggerService)
    {
        _slideManagementService = slideManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new Slide
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Slide")]
    public async Task<RequestResult<bool>?> CreateSlideAsync([FromForm]CreateSlideRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _slideManagementService.CreateSlideAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateSlideAsync));
            throw;
        }
    }

    /// <summary>
    /// Update Slide
    /// </summary>
    /// <param name="updateSlideRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-Slide")]
    public async Task<RequestResult<bool>?> UpdateSlideAsync([FromForm]UpdateSlideRequest updateSlideRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _slideManagementService.UpdateSlideAsync(updateSlideRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateSlideAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Delete Slide with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-Slide/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteSlideAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _slideManagementService.DeleteSlideAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteSlideAsync));
            throw;
        }
    }

    /// <summary>
    /// Get Slide with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-Slide/{id:long}")]
    public async Task<RequestResult<SlideResponse>?> ViewSlideAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _slideManagementService.GetSlideAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewSlideAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Slide with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Slides")]
    public async Task<RequestResult<OffsetPaginationResponse<SlideResponse>>> ViewListSlidesAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _slideManagementService.GetListSlidesAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListSlidesAsync));
            throw;
        }
    }
}