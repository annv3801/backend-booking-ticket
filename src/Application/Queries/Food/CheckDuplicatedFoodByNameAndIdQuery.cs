using MediatR;

namespace Application.Queries.Food;

public class CheckDuplicatedFoodByNameAndIdQuery : IRequest<bool>
{
    public string Title { get; set; }
    public long Id { get; set; }
}