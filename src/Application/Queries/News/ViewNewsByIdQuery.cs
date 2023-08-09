using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.News.Responses;
using MediatR;

namespace Application.Queries.News;
[ExcludeFromCodeCoverage]
public class ViewNewsByIdQuery : IRequest<Result<ViewNewsResponse>>
{
    public ViewNewsByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
