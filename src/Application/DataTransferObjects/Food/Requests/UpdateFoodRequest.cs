using System.Diagnostics.CodeAnalysis;
using Domain.Constants;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Food.Requests;

[ExcludeFromCodeCoverage]
public class UpdateFoodRequest
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public long GroupEntityId { get; set; }
}