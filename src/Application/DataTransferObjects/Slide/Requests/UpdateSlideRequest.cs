using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Slide.Requests;

[ExcludeFromCodeCoverage]
public class UpdateSlideRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public IFormFile? Image { get; set; }
    public long ObjectId { get; set; }
}