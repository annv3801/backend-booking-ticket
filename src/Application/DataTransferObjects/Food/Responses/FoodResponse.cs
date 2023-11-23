using Domain.Constants;
using Domain.Entities;

namespace Application.DataTransferObjects.Food.Responses;

public class FoodResponse 
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Status { get; set; }
    public long GroupEntityId { get; set; }
    public GroupEntity GroupEntity { get; set; }
}
