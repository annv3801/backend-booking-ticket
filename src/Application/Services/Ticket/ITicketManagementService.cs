using Application.DataTransferObjects.Ticket.Requests;
using Application.DataTransferObjects.Ticket.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Ticket;

public interface ITicketManagementService
{
    Task<RequestResult<bool>> CreateTicketAsync(CreateTicketRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateTicketAsync(UpdateTicketRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteTicketAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<TicketResponse>> GetTicketAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<TicketResponse>>> GetListTicketsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
}