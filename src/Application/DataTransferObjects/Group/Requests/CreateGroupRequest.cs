// ReSharper disable UnusedAutoPropertyAccessor.Global

using Domain.Constants;

namespace Application.DataTransferObjects.Group.Requests;

public class CreateGroupRequest
{
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = EntityGroup.Film;
    public int Index { get; set; }
}