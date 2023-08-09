using Application.Commands.FilmSchedules;
using MediatR;

namespace Application.Handlers.FilmSchedules;

public interface IUpdateFilmSchedulesCommandHandler : IRequestHandler<UpdateFilmSchedulesCommand, int>
{
}