using MediatR;

namespace Application.Commands.Food;

public class DeleteFoodCommand : IRequest<int>
{
    public required long Id { get; set; }
}