using System.Diagnostics.CodeAnalysis;
using Domain.Constants;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.Seat.Requests;

[ExcludeFromCodeCoverage]
public class UpdateSeatRequest
{
    public long Id { get; set; }
    public long SchedulerId { get; set; }
    public long RoomSeatId { get; set; }
    public long TicketId { get; set; }
    public int Type { get; set; }
}