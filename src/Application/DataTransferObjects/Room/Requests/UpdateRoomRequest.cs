using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Room.Requests;

[ExcludeFromCodeCoverage]
public class UpdateRoomRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long TheaterId { get; set; }
}