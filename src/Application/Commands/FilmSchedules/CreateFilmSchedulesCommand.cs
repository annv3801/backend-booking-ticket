using Application.DataTransferObjects.FilmSchedules.Requests;
using MediatR;

namespace Application.Commands.FilmSchedules;

public class CreateFilmSchedulesCommand : CreateFilmSchedulesRequest, IRequest<int>
{
    public CreateFilmSchedulesCommand(Domain.Entities.FilmSchedule entity)
    {
        Entity = entity;
    }

    public Domain.Entities.FilmSchedule Entity { get; set; }
}