using Application.DataTransferObjects.Film.Responses;
using MediatR;

namespace Application.Queries.Film;

public class GetFeedbackByFilmIdQuery : IRequest<FeedbackFilmResponse?>
{
    public long FilmId { get; set; }
}