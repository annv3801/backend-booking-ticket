// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Application.DataTransferObjects.Group.Requests;

public class CreateGroupRequest
{
    public string Title { get; set; } = string.Empty;
    public int Index { get; set; }
}