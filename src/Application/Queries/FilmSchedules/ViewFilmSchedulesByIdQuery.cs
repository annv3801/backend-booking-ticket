using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.FilmSchedules.Responses;
using MediatR;

namespace Application.Queries.FilmSchedules;
[ExcludeFromCodeCoverage]
public class ViewFilmSchedulesByIdQuery : IRequest<Result<ViewFilmSchedulesResponse>>
{
    public ViewFilmSchedulesByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
