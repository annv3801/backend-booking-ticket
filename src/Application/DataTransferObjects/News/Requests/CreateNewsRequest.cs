// ReSharper disable UnusedAutoPropertyAccessor.Global

using Microsoft.AspNetCore.Http;

namespace Application.DataTransferObjects.News.Requests;

public class CreateNewsRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public IFormFile? Image { get; set; }
}