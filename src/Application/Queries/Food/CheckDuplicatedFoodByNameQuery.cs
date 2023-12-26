using MediatR;

namespace Application.Queries.Food;

public class CheckDuplicatedFoodByNameQuery :  IRequest<bool>
{
    public string Title { get; set; }
}