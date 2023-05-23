using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Category.Responses;
[ExcludeFromCodeCoverage]
public class ViewCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortenUrl { get; set; }
    public int Status { get; set; }
}
