using System.Diagnostics.CodeAnalysis;
using Domain.Constants;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Application.DataTransferObjects.RoomSeat.Requests;

[ExcludeFromCodeCoverage]
public class UpdateRoomSeatRequest
{
    public long Id { get; set; }
    public required string Name { get; set; }
}