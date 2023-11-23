using Domain.Entities;
using MediatR;

namespace Application.Commands.Food;

public class CreateFoodCommand : IRequest<int>
{
    public required FoodEntity Entity { get; set; }
}