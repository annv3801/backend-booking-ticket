using MediatR;

namespace Application.Queries.Food;

public class CheckDuplicatedFoodByNameAndIdQuery : IRequest<bool>
{
    public required string Title { get; set; }
    public required long Id { get; set; }
}