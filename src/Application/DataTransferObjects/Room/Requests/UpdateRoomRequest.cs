using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Room.Requests;
[ExcludeFromCodeCoverage]
public class UpdateRoomRequest
{
    public string Name { get; set; }
    public Guid TheaterId { get; set; }
    public int Status { get; set; }
}
