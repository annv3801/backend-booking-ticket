using Application.DataTransferObjects.Theater.Requests;
using Application.Services.Theater;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class TheaterController : Controller
{
    private readonly ITheaterManagementService _theaterManagementService;

    public TheaterController(ITheaterManagementService theaterManagementService)
    {
        _theaterManagementService = theaterManagementService;
    }
    
    [HttpPost]
    [Route("create-theater")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateTheaterAsync(CreateTheaterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.CreateTheaterAsync(request, cancellationToken);
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
    [Route("delete-theater/{id}")]
    public async Task<IActionResult> DeleteTheaterAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.DeleteTheaterAsync(id, cancellationToken);
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
    [Route("update-theater/{id}")]
    public async Task<IActionResult> UpdateTheaterAsync(Guid id, UpdateTheaterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.UpdateTheaterAsync(id, request, cancellationToken);
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
    [Route("view-theater/{id}")]
    public async Task<IActionResult> ViewTheaterAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.ViewTheaterAsync(id, cancellationToken);
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
    [Route("view-list-theater")]
    // [Cached]
    public async Task<IActionResult>? ViewListTheatersAsync([FromQuery] ViewListTheatersRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _theaterManagementService.ViewListTheatersAsync(request, cancellationToken);
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