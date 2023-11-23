using Domain.Entities;
using MediatR;

namespace Application.Commands.Food;

public class UpdateFoodCommand : IRequest<int>
{
    public required FoodEntity Request { get; set; }
}