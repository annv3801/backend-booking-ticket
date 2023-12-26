using Domain.Entities;
using MediatR;

namespace Application.Commands.Film;

public class CreateFeedbackFilmCommand : IRequest<int>
{
    public FilmFeedbackEntity Entity { get; set; }
}