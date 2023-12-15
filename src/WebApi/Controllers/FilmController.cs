using Application.DataTransferObjects.Film.Requests;
using Application.DataTransferObjects.Film.Responses;
using Application.Repositories.Film;
using Application.Services.Film;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching Film, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Film")]
public class FilmController : ControllerBase
{
    private readonly IFilmManagementService _filmManagementService;
    private readonly ILoggerService _loggerService;

    public FilmController(IFilmManagementService filmManagementService, ILoggerService loggerService)
    {
        _filmManagementService = filmManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new Film
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Film")]
    public async Task<RequestResult<bool>?> CreateFilmAsync([FromForm]CreateFilmRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.CreateFilmAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateFilmAsync));
            throw;
        }
    }
    
    /// <summary>
    /// Update Film
    /// </summary>
    /// <param name="updateFilmRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-Film")]
    public async Task<RequestResult<bool>?> UpdateFilmAsync([FromForm]UpdateFilmRequest updateFilmRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.UpdateFilmAsync(updateFilmRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateFilmAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    
    /// <summary>
    /// Delete Film with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-Film/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteFilmAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.DeleteFilmAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteFilmAsync));
            throw;
        }
    }
    
    /// <summary>
    /// Get Film with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-Film/{id:long}")]
    public async Task<RequestResult<FilmResponse>?> ViewFilmAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.GetFilmAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewFilmAsync));
            throw;
        }
    }
    
    /// <summary>
    /// Get list Film with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Films-By-Group")]
    public async Task<RequestResult<OffsetPaginationResponse<FilmResponse>>> ViewListFilmsByGroupAsync(ViewListFilmsByGroupRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.GetListFilmsByGroupAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListFilmsByGroupAsync));
            throw;
        }
    }
    
    /// <summary>
    /// Create film feedback
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Film-Feedback")]
    public async Task<RequestResult<bool>?> CreateFilmFeedbackAsync(CreateFeedbackFilmRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.CreateFeedbackFilmAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateFilmFeedbackAsync));
            throw;
        }
    }
}