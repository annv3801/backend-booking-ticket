// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Application.DataTransferObjects.Category.Requests;

public class CreateCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}