using Domain.Entities;
using MediatR;

namespace Application.Commands.Film;

public class CreateFeedbackFilmCommand : IRequest<int>
{
    public required FilmFeedbackEntity Entity { get; set; }
}