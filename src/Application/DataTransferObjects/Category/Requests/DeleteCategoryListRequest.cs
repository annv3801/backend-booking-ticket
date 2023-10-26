using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Category.Requests;

[ExcludeFromCodeCoverage]
public class DeleteCategoryListRequest
{
    public required ICollection<long> CategoryIds { get; set; }
}