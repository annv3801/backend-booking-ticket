using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Category.Responses;
using MediatR;

namespace Application.Queries.Category;
[ExcludeFromCodeCoverage]
public class ViewCategoryByIdQuery : IRequest<Result<ViewCategoryResponse>>
{
    public ViewCategoryByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
