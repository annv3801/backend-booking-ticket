using Application.DataTransferObjects.FilmSchedules.Requests;
using Application.Services.FilmSchedules;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class FilmSchedulesController : Controller
{
    private readonly IFilmSchedulesManagementService _filmManagementService;

    public FilmSchedulesController(IFilmSchedulesManagementService filmManagementService)
    {
        _filmManagementService = filmManagementService;
    }
    
    [HttpPost]
    [Route("create-schedule")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateFilmSchedulesAsync(CreateFilmSchedulesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.CreateFilmSchedulesAsync(request, cancellationToken);
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
    [Route("delete-schedule/{id}")]
    public async Task<IActionResult> DeleteFilmSchedulesAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.DeleteFilmSchedulesAsync(id, cancellationToken);
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
    [Route("update-schedule/{id}")]
    public async Task<IActionResult> UpdateFilmSchedulesAsync(Guid id, UpdateFilmSchedulesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.UpdateFilmSchedulesAsync(id, request, cancellationToken);
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
    [Route("view-schedule/{id}")]
    public async Task<IActionResult> ViewFilmSchedulesAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.ViewFilmSchedulesAsync(id, cancellationToken);
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
    [Route("view-list-schedule")]
    // [Cached]
    public async Task<IActionResult>? ViewListFilmSchedulessAsync([FromQuery] ViewListFilmSchedulesRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _filmManagementService.ViewListFilmSchedulesAsync(request, cancellationToken);
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