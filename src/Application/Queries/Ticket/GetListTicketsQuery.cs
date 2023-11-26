using Application.DataTransferObjects.Ticket.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Ticket;

public class GetListTicketsQuery : IRequest<OffsetPaginationResponse<TicketResponse>>
{
    public required OffsetPaginationRequest OffsetPaginationRequest { get; set; }
}