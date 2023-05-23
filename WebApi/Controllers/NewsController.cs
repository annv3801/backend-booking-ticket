using Application.Common.Interfaces;
using Application.DataTransferObjects.News.Requests;
using Application.Services.News;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class NewsController : Controller
{
    private readonly INewsManagementService _newsManagementService;

    public NewsController(INewsManagementService newsManagementService)
    {
        _newsManagementService = newsManagementService;
    }
    
    [HttpPost]
    [Route("create-news")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateNewsAsync([FromForm]CreateNewsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _newsManagementService.CreateNewsAsync(request, cancellationToken);
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
    [Route("delete-news/{id}")]
    public async Task<IActionResult> DeleteNewsAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _newsManagementService.DeleteNewsAsync(id, cancellationToken);
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
    [Route("update-news/{id}")]
    public async Task<IActionResult> UpdateNewsAsync(Guid id, UpdateNewsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _newsManagementService.UpdateNewsAsync(id, request, cancellationToken);
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
    [Route("view-news/{id}")]
    public async Task<IActionResult> ViewNewsAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _newsManagementService.ViewNewsAsync(id, cancellationToken);
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
    [Route("view-list-news")]
    // [Cached]
    public async Task<IActionResult>? ViewListNewssAsync([FromQuery] ViewListNewsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _newsManagementService.ViewListNewsAsync(request, cancellationToken);
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