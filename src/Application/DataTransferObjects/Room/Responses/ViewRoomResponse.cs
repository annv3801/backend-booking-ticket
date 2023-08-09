using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Room.Responses;
[ExcludeFromCodeCoverage]
public class ViewRoomResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? Status { get; set; }
    public Guid TheaterId { get; set; }
    public string TheaterName { get; set; }
    public string TheaterAddress { get; set; }
}
