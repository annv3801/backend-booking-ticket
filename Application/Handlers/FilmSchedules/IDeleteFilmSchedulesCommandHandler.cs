using Application.Commands.FilmSchedules;
using MediatR;

namespace Application.Handlers.FilmSchedules;

public interface IDeleteFilmSchedulesCommandHandler : IRequestHandler<DeleteFilmSchedulesCommand, int>
{
}