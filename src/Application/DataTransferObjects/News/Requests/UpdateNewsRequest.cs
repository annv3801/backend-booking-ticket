using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.News.Requests;

[ExcludeFromCodeCoverage]
public class UpdateNewsRequest
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public long GroupEntityId { get; set; }
    public IFormFile? Image { get; set; }
}