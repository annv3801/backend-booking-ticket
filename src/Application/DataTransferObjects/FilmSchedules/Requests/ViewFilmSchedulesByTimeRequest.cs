using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.FilmSchedules.Requests;
[ExcludeFromCodeCoverage]
public class ViewListFilmSchedulesByTimeRequest
{
    public DateTime Date { get; set; }
    public Guid FilmId { get; set; }
}
