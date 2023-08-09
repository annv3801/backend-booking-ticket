using MediatR;

namespace Application.Commands.Film;

public class DeleteFilmCommand : IRequest<int>
{
    public DeleteFilmCommand(Domain.Entities.Film entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Film Entity { get; set; }
}