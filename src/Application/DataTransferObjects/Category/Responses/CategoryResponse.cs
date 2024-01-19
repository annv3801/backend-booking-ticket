using Domain.Constants;

namespace Application.DataTransferObjects.Category.Responses;

public class CategoryResponse 
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = EntityStatus.Active;
    public DateTimeOffset? CreatedTime { get; set; }
}
