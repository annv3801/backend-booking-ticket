using Application.DataTransferObjects.Film.Responses;
using MediatR;

namespace Application.Queries.Film;

public class GetFilmByIdQuery : IRequest<FilmResponse?>
{
    public long Id { get; set; }
}