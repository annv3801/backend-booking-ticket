using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.News.Requests;
[ExcludeFromCodeCoverage]
public class UpdateNewsRequest
{
    public string CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
}
