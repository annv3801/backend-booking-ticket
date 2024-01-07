using Application.DataTransferObjects.News.Requests;
using Application.DataTransferObjects.News.Responses;
using Application.Services.News;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching News, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("News")]
public class NewsController : ControllerBase
{
    private readonly INewsManagementService _newsManagementService;
    private readonly ILoggerService _loggerService;

    public NewsController(INewsManagementService newsManagementService, ILoggerService loggerService)
    {
        _newsManagementService = newsManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new News
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-News")]
    public async Task<RequestResult<bool>?> CreateNewsAsync([FromForm]CreateNewsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _newsManagementService.CreateNewsAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateNewsAsync));
            throw;
        }
    }

    /// <summary>
    /// Update News
    /// </summary>
    /// <param name="updateNewsRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-News")]
    public async Task<RequestResult<bool>?> UpdateNewsAsync([FromForm]UpdateNewsRequest updateNewsRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _newsManagementService.UpdateNewsAsync(updateNewsRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateNewsAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Delete News with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-News/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteNewsAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _newsManagementService.DeleteNewsAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteNewsAsync));
            throw;
        }
    }

    /// <summary>
    /// Get News with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-News/{id:long}")]
    public async Task<RequestResult<NewsResponse>?> ViewNewsAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _newsManagementService.GetNewsAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewNewsAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list News with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-News")]
    public async Task<RequestResult<OffsetPaginationResponse<NewsResponse>>> ViewListNewsAsync(ViewNewsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _newsManagementService.GetListNewsAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListNewsAsync));
            throw;
        }
    }
}