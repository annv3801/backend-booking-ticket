using Application.Common.Interfaces;
using Application.DataTransferObjects.Category.Requests;
using Application.Services.Category;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class CategoryController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ICategoryManagementService _categoryManagementService;

    public CategoryController(IMediator mediator, IMapper mapper, ICurrentAccountService currentAccountService, ICategoryManagementService categoryManagementService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
        _categoryManagementService = categoryManagementService;
    }
    
    [HttpPost]
    [Route("create-category")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateAccountAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryManagementService.CreateCategoryAsync(request, cancellationToken);
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
    [Route("delete-category/{id}")]
    public async Task<IActionResult> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryManagementService.DeleteCategoryAsync(id, cancellationToken);
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
    [Route("update-category/{id}")]
    public async Task<IActionResult> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryManagementService.UpdateCategoryAsync(id, request, cancellationToken);
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
    [Route("view-category/{id}")]
    public async Task<IActionResult> ViewCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryManagementService.ViewCategoryAsync(id, cancellationToken);
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
    [Route("view-list-category")]
    // [Cached]
    public async Task<IActionResult>? ViewListCategoriesAsync([FromQuery] ViewListCategoriesRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _categoryManagementService.ViewListCategoriesAsync(request, cancellationToken);
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