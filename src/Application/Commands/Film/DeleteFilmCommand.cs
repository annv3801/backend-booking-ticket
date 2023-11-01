using MediatR;

namespace Application.Commands.Film;

public class DeleteFilmCommand : IRequest<int>
{
    public required long Id { get; set; }
}