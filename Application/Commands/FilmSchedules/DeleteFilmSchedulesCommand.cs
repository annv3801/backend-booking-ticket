using MediatR;

namespace Application.Commands.FilmSchedules;

public class DeleteFilmSchedulesCommand : IRequest<int>
{
    public DeleteFilmSchedulesCommand(Domain.Entities.FilmSchedule entity)
    {
        Entity = entity;
    }

    public Domain.Entities.FilmSchedule Entity { get; set; }
}