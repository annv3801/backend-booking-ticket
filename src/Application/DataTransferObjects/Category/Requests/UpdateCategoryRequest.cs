using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Category.Requests;

[ExcludeFromCodeCoverage]
public class UpdateCategoryRequest
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}