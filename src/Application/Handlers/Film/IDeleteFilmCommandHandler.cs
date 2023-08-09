using Application.Commands.Film;
using MediatR;

namespace Application.Handlers.Film;

public interface IDeleteFilmCommandHandler : IRequestHandler<DeleteFilmCommand, int>
{
}