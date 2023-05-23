using Application.Common.Interfaces;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Film.Requests;
using Application.Queries.Category;
using Application.Queries.Film;
using Application.Services.Category;
using Application.Services.Film;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class FilmController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IFilmManagementService _filmManagementService;

    public FilmController(IMediator mediator, IMapper mapper, ICurrentAccountService currentAccountService, IFilmManagementService filmManagementService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _currentAccountService = currentAccountService;
        _filmManagementService = filmManagementService;
    }
    
    [HttpPost]
    [Route("create-film")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateFilmAsync([FromForm]CreateFilmRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.CreateFilmAsync(request, cancellationToken);
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
    [Route("delete-film/{id}")]
    public async Task<IActionResult> DeleteFilmAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.DeleteFilmAsync(id, cancellationToken);
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
    [Route("update-film/{id}")]
    public async Task<IActionResult> UpdateFilmAsync(Guid id, UpdateFilmRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.UpdateFilmAsync(id, request, cancellationToken);
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
    [Route("view-film/{id}")]
    public async Task<IActionResult> ViewFilmAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.ViewFilmAsync(id, cancellationToken);
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
    [Route("view-film-by-shorten-url/{shortenUrl}")]
    public async Task<IActionResult> ViewFilmByShortenUrlAsync(string shortenUrl, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.ViewFilmByShortenUrlAsync(shortenUrl, cancellationToken);
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
    [Route("view-list-film")]
    // [Cached]
    public async Task<IActionResult>? ViewListFilmsAsync([FromQuery] ViewListFilmsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _filmManagementService.ViewListFilmsAsync(request, cancellationToken);
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
    [Route("view-list-film-by-category")]
    // [Cached]
    public async Task<IActionResult>? ViewListFilmsByCategoryAsync([FromQuery] ViewListFilmByCategoryQuery request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _filmManagementService.ViewListFilmByCategoryAsync(request, cancellationToken);
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