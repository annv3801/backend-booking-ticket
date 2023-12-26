using MediatR;

namespace Application.Commands.Film;

public class DeleteFilmCommand : IRequest<int>
{
    public long Id { get; set; }
}