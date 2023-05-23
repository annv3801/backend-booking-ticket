using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Theater.Responses;
using MediatR;

namespace Application.Queries.Theater;
[ExcludeFromCodeCoverage]
public class ViewTheaterByIdQuery : IRequest<Result<ViewTheaterResponse>>
{
    public ViewTheaterByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
