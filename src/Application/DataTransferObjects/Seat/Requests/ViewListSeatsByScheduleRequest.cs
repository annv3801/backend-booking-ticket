using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Pagination.Requests;

namespace Application.DataTransferObjects.Seat.Requests;
[ExcludeFromCodeCoverage]
public class ViewListSeatsByScheduleRequest : PaginationBaseRequest
{
    public Guid ScheduleId { get; set; }
}
