using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.FilmSchedules.Responses;
using MediatR;

namespace Application.Queries.FilmSchedules;
[ExcludeFromCodeCoverage]
public class ViewListFilmSchedulesByTimeQuery : IRequest<Result<List<TheaterScheduleResponse>>>
{
    public DateTime Date { get; set; }
    public Guid FilmId { get; set; }
}
