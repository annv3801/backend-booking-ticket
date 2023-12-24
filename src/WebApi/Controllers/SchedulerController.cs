using Application.DataTransferObjects.Scheduler.Requests;
using Application.DataTransferObjects.Scheduler.Responses;
using Application.Services.Scheduler;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching Scheduler, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Scheduler")]
public class SchedulerController : ControllerBase
{
    private readonly ISchedulerManagementService _schedulerManagementService;
    private readonly ILoggerService _loggerService;

    public SchedulerController(ISchedulerManagementService schedulerManagementService, ILoggerService loggerService)
    {
        _schedulerManagementService = schedulerManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new Scheduler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Scheduler")]
    public async Task<RequestResult<bool>?> CreateSchedulerAsync(CreateSchedulerRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _schedulerManagementService.CreateSchedulerAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateSchedulerAsync));
            throw;
        }
    }

    /// <summary>
    /// Update Scheduler
    /// </summary>
    /// <param name="updateSchedulerRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-Scheduler")]
    public async Task<RequestResult<bool>?> UpdateSchedulerAsync(UpdateSchedulerRequest updateSchedulerRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _schedulerManagementService.UpdateSchedulerAsync(updateSchedulerRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateSchedulerAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Delete Scheduler with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-Scheduler/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteSchedulerAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _schedulerManagementService.DeleteSchedulerAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteSchedulerAsync));
            throw;
        }
    }

    /// <summary>
    /// Get Scheduler with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-Scheduler/{id:long}")]
    public async Task<RequestResult<SchedulerResponse>?> ViewSchedulerAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _schedulerManagementService.GetSchedulerAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewSchedulerAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Scheduler with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Schedulers")]
    public async Task<RequestResult<OffsetPaginationResponse<SchedulerResponse>>> ViewListSchedulersAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _schedulerManagementService.GetListSchedulersAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListSchedulersAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Scheduler with pagination
    /// </summary>
    /// <param name="date"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="theaterId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-List-Schedulers/{theaterId:long}")]
    public async Task<RequestResult<ICollection<SchedulerGroupResponse>>> ViewListSchedulersByTheaterAsync(long theaterId,[FromQuery] string date, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _schedulerManagementService.GetSchedulerByTheaterIdAndDateAsync(theaterId, date, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListSchedulersAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Scheduler with pagination
    /// </summary>
    /// <param name="date"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="filmId"></param>
    /// <param name="theaterId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-List-Schedulers/{theaterId:long}/{filmId:long}")]
    public async Task<RequestResult<ICollection<SchedulerGroupResponse>>> ViewListSchedulersByFilmAsync(long filmId, long theaterId, [FromQuery] string date, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _schedulerManagementService.GetSchedulerByTheaterIdAndDateAndFilmIdAsync(theaterId, date, filmId, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListSchedulersAsync));
            throw;
        }
    }
    /// <summary>
    /// Get list Scheduler with pagination
    /// </summary>
    /// <param name="date"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="filmId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-List-Schedulers-By-Film/{filmId:long}")]
    public async Task<RequestResult<ICollection<SchedulerGroupResponse>>> ViewListSchedulersByFilmAsync(long filmId, [FromQuery] string date, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _schedulerManagementService.GetSchedulerByDateAndFilmIdAsync(date, filmId, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListSchedulersAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Scheduler with pagination
    /// </summary>
    /// <param name="date"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="request"></param>
    /// <param name="filmId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Theater-By-Film/{filmId:long}")]
    public async Task<RequestResult<OffsetPaginationResponse<SchedulerFilmAndTheaterResponse>>> ViewListTheatersByFilmAsync(OffsetPaginationRequest request, long filmId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _schedulerManagementService.GetListTheaterByFilmAsync(request, filmId, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListTheatersByFilmAsync));
            throw;
        }
    }
}