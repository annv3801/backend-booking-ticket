using System.Diagnostics.CodeAnalysis;
using Domain.Constants;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Group.Requests;

[ExcludeFromCodeCoverage]
public class UpdateGroupRequest
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = EntityGroup.Film;
    public int Index { get; set; }
}