using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Film.Responses;
using MediatR;

namespace Application.Queries.Film;
[ExcludeFromCodeCoverage]
public class ViewFilmByIdQuery : IRequest<Result<ViewFilmResponse>>
{
    public ViewFilmByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
