using System.Diagnostics.CodeAnalysis;
using Domain.Common.Pagination.OffsetBased;

namespace Application.DataTransferObjects.Booking.Requests;
[ExcludeFromCodeCoverage]
public class ViewListBookingsRequest : OffsetPaginationRequest
{
    public long? AccountId { get; set; }
}
