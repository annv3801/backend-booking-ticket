using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Seat.Requests;
[ExcludeFromCodeCoverage]
public class UpdateSeatRequest
{
    public string Name { get; set; }
    public Guid RoomId { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }
}
