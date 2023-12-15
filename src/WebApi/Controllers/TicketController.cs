using Application.DataTransferObjects.Ticket.Requests;
using Application.DataTransferObjects.Ticket.Responses;
using Application.Services.Ticket;
using Domain.Common.Interface;
using Domain.Common.Pagination.OffsetBased;
using Microsoft.AspNetCore.Mvc;
using Nobi.Core.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Provides API endpoints for managing Categories. Supports creating, updating, 
/// deleting and fetching Ticket, and listing Categories with pagination. 
/// Exceptions are logged using an injected ILoggerService instance.
/// </summary>
[ApiController]
[Route("Ticket")]
public class TicketController : ControllerBase
{
    private readonly ITicketManagementService _ticketManagementService;
    private readonly ILoggerService _loggerService;

    public TicketController(ITicketManagementService ticketManagementService, ILoggerService loggerService)
    {
        _ticketManagementService = ticketManagementService;
        _loggerService = loggerService;
    }

    /// <summary>
    /// Create new Ticket
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Create-Ticket")]
    public async Task<RequestResult<bool>?> CreateTicketAsync(CreateTicketRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ticketManagementService.CreateTicketAsync(request,  cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(CreateTicketAsync));
            throw;
        }
    }

    /// <summary>
    /// Update Ticket
    /// </summary>
    /// <param name="updateTicketRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Update-Ticket")]
    public async Task<RequestResult<bool>?> UpdateTicketAsync(UpdateTicketRequest updateTicketRequest, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ticketManagementService.UpdateTicketAsync(updateTicketRequest, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(UpdateTicketAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Delete Ticket with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Delete-Ticket/{id:long}")]
    public async Task<RequestResult<bool>?> DeleteTicketAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ticketManagementService.DeleteTicketAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(DeleteTicketAsync));
            throw;
        }
    }

    /// <summary>
    /// Get Ticket with id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("View-Ticket/{id:long}")]
    public async Task<RequestResult<TicketResponse>?> ViewTicketAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ticketManagementService.GetTicketAsync(id, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewTicketAsync));
            throw;
        }
    }

    /// <summary>
    /// Get list Ticket with pagination
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("View-List-Tickets")]
    public async Task<RequestResult<OffsetPaginationResponse<TicketResponse>>> ViewListTicketsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ticketManagementService.GetListTicketsAsync(request, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _loggerService.LogError(e, nameof(ViewListTicketsAsync));
            throw;
        }
    }
}