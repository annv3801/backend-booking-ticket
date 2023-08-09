using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Film.Requests;
using MediatR;

namespace Application.Commands.Film;
[ExcludeFromCodeCoverage]
public class UpdateFilmCommand : UpdateFilmRequest, IRequest<int>
{
    public UpdateFilmCommand(Domain.Entities.Film entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Film Entity { get; set; }
}
