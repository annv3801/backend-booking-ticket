using Application.DataTransferObjects.Theater.Requests;
using Application.DataTransferObjects.Theater.Responses;
using Application.Services.Theater;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching Theater, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Theater")]
public class TheaterController : ControllerBase
{
    private readonly ITheaterManagementService _theaterManagementService;
    private readonly ILoggerService _loggerService;

    public TheaterController(ITheaterManagementService theaterManagementService, ILoggerService loggerService)
    {
        _theaterManagementService = theaterManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new Theater
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Theater")]
    public async Task<RequestResult<bool>?> CreateTheaterAsync(CreateTheaterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.CreateTheaterAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateTheaterAsync));
            throw;
        }
    }

    /// <summary>
    /// Update Theater
    /// </summary>
    /// <param name="updateTheaterRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-Theater")]
    public async Task<RequestResult<bool>?> UpdateTheaterAsync(UpdateTheaterRequest updateTheaterRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.UpdateTheaterAsync(updateTheaterRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateTheaterAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Delete Theater with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-Theater/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteTheaterAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.DeleteTheaterAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteTheaterAsync));
            throw;
        }
    }

    /// <summary>
    /// Get Theater with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-Theater/{id:long}")]
    public async Task<RequestResult<TheaterResponse>?> ViewTheaterAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.GetTheaterAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewTheaterAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Theater with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Theaters")]
    public async Task<RequestResult<OffsetPaginationResponse<TheaterResponse>>> ViewListTheatersAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.GetListTheatersAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListTheatersAsync));
            throw;
        }
    }
    
    /// <summary>
    /// Get list Theater with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Theaters-Favorites")]
    public async Task<RequestResult<OffsetPaginationResponse<TheaterResponse>>> ViewListTheatersFavoritesAsync(ViewTheaterFavoriteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.GetListTheatersFavoritesAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListTheatersFavoritesAsync));
            throw;
        }
    }
}