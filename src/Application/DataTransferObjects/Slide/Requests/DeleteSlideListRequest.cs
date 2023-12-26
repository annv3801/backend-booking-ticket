using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Slide.Requests;

[ExcludeFromCodeCoverage]
public class DeleteSlideListRequest
{
    public ICollection<long> SlideIds { get; set; }
}