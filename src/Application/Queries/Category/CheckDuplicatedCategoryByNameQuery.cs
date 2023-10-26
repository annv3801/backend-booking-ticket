using MediatR;

namespace Application.Queries.Category;

public class CheckDuplicatedCategoryByNameQuery :  IRequest<bool>
{
    public required string Name { get; set; }
}