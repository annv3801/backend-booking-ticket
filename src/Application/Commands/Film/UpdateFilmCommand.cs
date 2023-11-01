using Domain.Entities;
using MediatR;

namespace Application.Commands.Film;

public class UpdateFilmCommand : IRequest<int>
{
    public required FilmEntity Request { get; set; }
}