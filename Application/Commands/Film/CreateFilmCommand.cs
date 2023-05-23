using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Film.Requests;
using MediatR;

namespace Application.Commands.Film;

public class CreateFilmCommand : CreateFilmRequest, IRequest<int>
{
    public CreateFilmCommand(Domain.Entities.Film entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Film Entity { get; set; }
}