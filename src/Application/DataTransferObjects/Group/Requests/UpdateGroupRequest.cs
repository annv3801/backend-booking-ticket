using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Group.Requests;

[ExcludeFromCodeCoverage]
public class UpdateGroupRequest
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Index { get; set; }
}