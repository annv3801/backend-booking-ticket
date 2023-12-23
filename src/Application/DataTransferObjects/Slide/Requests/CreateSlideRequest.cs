// ReSharper disable UnusedAutoPropertyAccessor.Global

using Microsoft.AspNetCore.Http;

namespace Application.DataTransferObjects.Slide.Requests;

public class CreateSlideRequest
{
    public string Name { get; set; }
    public IFormFile? Image { get; set; }
    public long ObjectId { get; set; }
}