using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Category.Requests;

[ExcludeFromCodeCoverage]
public class DeleteCategoryListRequest
{
    public ICollection<long> CategoryIds { get; set; }
}