using MediatR;

namespace Application.Commands.Food;

public class DeleteFoodCommand : IRequest<int>
{
    public long Id { get; set; }
}