using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Slide.Requests;

[ExcludeFromCodeCoverage]
public class DeleteSlideListRequest
{
    public required ICollection<long> SlideIds { get; set; }
}