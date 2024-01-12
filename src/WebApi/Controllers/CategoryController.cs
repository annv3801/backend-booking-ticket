using Application.Common;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Category.Responses;
using Application.Services.Category;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;
using WebApi.Services;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching Category, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryManagementService _categoryManagementService;
    private readonly ILoggerService _loggerService;

    public CategoryController(ICategoryManagementService categoryManagementService, ILoggerService loggerService)
    {
        _categoryManagementService = categoryManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new Category
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Category")]
    public async Task<RequestResult<bool>?> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryManagementService.CreateCategoryAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateCategoryAsync));
            throw;
        }
    }

    /// <summary>
    /// Update Category
    /// </summary>
    /// <param name="updateCategoryRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-Category")]
    public async Task<RequestResult<bool>?> UpdateCategoryAsync(UpdateCategoryRequest updateCategoryRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryManagementService.UpdateCategoryAsync(updateCategoryRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateCategoryAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Delete Category with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-Category/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteCategoryAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            
            var result = await _categoryManagementService.DeleteCategoryAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteCategoryAsync));
            throw;
        }
    }

    /// <summary>
    /// Get Category with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-Category/{id:long}")]
    public async Task<RequestResult<CategoryResponse>?> ViewCategoryAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryManagementService.GetCategoryAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewCategoryAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Category with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Categories")]
    public async Task<RequestResult<OffsetPaginationResponse<CategoryResponse>>> ViewListCategoriesAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryManagementService.GetListCategoriesAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListCategoriesAsync));
            throw;
        }
    }
}