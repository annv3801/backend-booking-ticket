using Application.Commands.Film;
using MediatR;

namespace Application.Handlers.Film;

public interface ICreateFilmCommandHandler: IRequestHandler<CreateFilmCommand, int>
{
    
}