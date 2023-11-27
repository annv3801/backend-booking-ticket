using System.Diagnostics.CodeAnalysis;
using Domain.Common.Pagination.OffsetBased;

namespace Application.DataTransferObjects.Seat.Requests;

[ExcludeFromCodeCoverage]
public class ViewSeatRequest
{
    public OffsetPaginationRequest Request { get; set; }
}