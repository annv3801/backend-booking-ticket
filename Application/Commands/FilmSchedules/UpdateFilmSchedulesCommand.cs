using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.FilmSchedules.Requests;
using MediatR;

namespace Application.Commands.FilmSchedules;
[ExcludeFromCodeCoverage]
public class UpdateFilmSchedulesCommand : UpdateFilmSchedulesRequest, IRequest<int>
{
    public UpdateFilmSchedulesCommand(Domain.Entities.FilmSchedule entity)
    {
        Entity = entity;
    }

    public Domain.Entities.FilmSchedule Entity { get; set; }
}
