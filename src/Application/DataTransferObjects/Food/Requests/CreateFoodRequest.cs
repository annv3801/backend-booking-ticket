// ReSharper disable UnusedAutoPropertyAccessor.Global

using Domain.Constants;

namespace Application.DataTransferObjects.Food.Requests;

public class CreateFoodRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public long GroupEntityId { get; set; }
}