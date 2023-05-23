using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Category.Requests;
[ExcludeFromCodeCoverage]
public class UpdateCategoryRequest
{
    public string Name { get; set; } = "";
    public string ShortenUrl { get; set; } = "";
    public int Status { get; set; } = 1;
}
