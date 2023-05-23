using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Ticket.Requests;
using Application.DataTransferObjects.Ticket.Responses;

namespace Application.Services.Ticket;
public interface ITicketManagementService
{
    Task<Result<TicketResult>> CreateTicketAsync(CreateTicketRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewTicketResponse>> ViewTicketAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<TicketResult>> DeleteTicketAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<TicketResult>> UpdateTicketAsync(Guid id, UpdateTicketRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewTicketResponse>>> ViewListTicketsAsync(ViewListTicketsRequest request, CancellationToken cancellationToken = default(CancellationToken));

}
