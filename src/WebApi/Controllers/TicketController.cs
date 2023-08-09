using Application.DataTransferObjects.Ticket.Requests;
using Application.Services.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
public class TicketController : Controller
{
    private readonly ITicketManagementService _ticketManagementService;

    public TicketController(ITicketManagementService ticketManagementService)
    {
        _ticketManagementService = ticketManagementService;
    }
    
    [HttpPost]
    [Route("create-ticket")]
    [Produces("application/json")]

    public async Task<IActionResult> CreateTicketAsync(CreateTicketRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ticketManagementService.CreateTicketAsync(request, cancellationToken);
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
    [Route("delete-ticket/{id}")]
    public async Task<IActionResult> DeleteTicketAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ticketManagementService.DeleteTicketAsync(id, cancellationToken);
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
    [Route("update-ticket/{id}")]
    public async Task<IActionResult> UpdateTicketAsync(Guid id, UpdateTicketRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ticketManagementService.UpdateTicketAsync(id, request, cancellationToken);
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
    [Route("view-ticket/{id}")]
    public async Task<IActionResult> ViewTicketAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ticketManagementService.ViewTicketAsync(id, cancellationToken);
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
    [Route("view-list-ticket")]
    // [Cached]
    public async Task<IActionResult>? ViewListTicketsAsync([FromQuery] ViewListTicketsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _ticketManagementService.ViewListTicketsAsync(request, cancellationToken);
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