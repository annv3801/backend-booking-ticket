using MediatR;

namespace Application.Queries.Category;

public class CheckDuplicatedCategoryByNameQuery :  IRequest<bool>
{
    public string Name { get; set; }
}