using System.Diagnostics.CodeAnalysis;
using Domain.Common.Pagination.OffsetBased;

namespace Application.DataTransferObjects.Ticket.Requests;

[ExcludeFromCodeCoverage]
public class ViewTicketRequest
{
    public OffsetPaginationRequest Request { get; set; }
}