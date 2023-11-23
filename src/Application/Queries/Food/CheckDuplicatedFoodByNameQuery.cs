using MediatR;

namespace Application.Queries.Food;

public class CheckDuplicatedFoodByNameQuery :  IRequest<bool>
{
    public required string Title { get; set; }
}