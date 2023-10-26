using Application.DataTransferObjects.Category.Responses;
using MediatR;

namespace Application.Queries.Category;

public class GetCategoryByIdQuery : IRequest<CategoryResponse?>
{
    public long Id { get; set; }
}