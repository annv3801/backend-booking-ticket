using Application.Commands.FilmSchedules;
using MediatR;

namespace Application.Handlers.FilmSchedules;

public interface ICreateFilmSchedulesCommandHandler: IRequestHandler<CreateFilmSchedulesCommand, int>
{
    
}