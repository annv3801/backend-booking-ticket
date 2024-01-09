using Domain.Entities;

namespace Application.DataTransferObjects.News.Responses;

public class NewsResponse 
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string Status { get; set; }
    public string Image { get; set; }
    public DateTimeOffset? CreatedTime { get; set; }
    public long? CreatedBy { get; set; }
}
