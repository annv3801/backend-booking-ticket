using Application.DataTransferObjects.Food.Responses;
using MediatR;

namespace Application.Queries.Food;

public class GetFoodByIdQuery : IRequest<FoodResponse?>
{
    public long Id { get; set; }
}