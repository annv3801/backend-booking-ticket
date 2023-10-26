using MediatR;

namespace Application.Queries.Category;

public class CheckDuplicatedCategoryByNameAndIdQuery : IRequest<bool>
{
    public required string Name { get; set; }
    public required long Id { get; set; }
}