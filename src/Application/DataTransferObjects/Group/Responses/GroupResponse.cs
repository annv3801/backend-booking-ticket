using Domain.Constants;

namespace Application.DataTransferObjects.Group.Responses;

public class GroupResponse 
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Index { get; set; }
    public string Status { get; set; } = EntityStatus.Active;
}
