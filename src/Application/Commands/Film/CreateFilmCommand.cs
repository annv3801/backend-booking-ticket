using Domain.Entities;
using MediatR;

namespace Application.Commands.Film;

public class CreateFilmCommand : IRequest<int>
{
    public FilmEntity Entity { get; set; }
}