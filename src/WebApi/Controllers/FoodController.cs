using Application.DataTransferObjects.Food.Requests;
using Application.DataTransferObjects.Food.Responses;
using Application.Services.Food;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching Food, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Food")]
public class FoodController : ControllerBase
{
    private readonly IFoodManagementService _foodManagementService;
    private readonly ILoggerService _loggerService;

    public FoodController(IFoodManagementService foodManagementService, ILoggerService loggerService)
    {
        _foodManagementService = foodManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new Food
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Food")]
    public async Task<RequestResult<bool>?> CreateFoodAsync([FromForm]CreateFoodRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _foodManagementService.CreateFoodAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateFoodAsync));
            throw;
        }
    }

    /// <summary>
    /// Update Food
    /// </summary>
    /// <param name="updateFoodRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-Food")]
    public async Task<RequestResult<bool>?> UpdateFoodAsync([FromForm]UpdateFoodRequest updateFoodRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _foodManagementService.UpdateFoodAsync(updateFoodRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateFoodAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Delete Food with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-Food/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteFoodAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _foodManagementService.DeleteFoodAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteFoodAsync));
            throw;
        }
    }

    /// <summary>
    /// Get Food with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-Food/{id:long}")]
    public async Task<RequestResult<FoodResponse>?> ViewFoodAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _foodManagementService.GetFoodAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewFoodAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Food with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Foods")]
    public async Task<RequestResult<OffsetPaginationResponse<FoodResponse>>> ViewListFoodsAsync(ViewFoodRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _foodManagementService.GetListFoodsAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListFoodsAsync));
            throw;
        }
    }
}