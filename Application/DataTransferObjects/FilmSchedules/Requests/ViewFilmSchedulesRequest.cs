using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Pagination.Requests;

namespace Application.DataTransferObjects.FilmSchedules.Requests;
[ExcludeFromCodeCoverage]
public class ViewListFilmSchedulesRequest : PaginationBaseRequest
{
    public Guid ScheduleId { get; set; }
}
