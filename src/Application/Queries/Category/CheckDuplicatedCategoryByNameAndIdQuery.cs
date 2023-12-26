using MediatR;

namespace Application.Queries.Category;

public class CheckDuplicatedCategoryByNameAndIdQuery : IRequest<bool>
{
    public string Name { get; set; }
    public long Id { get; set; }
}