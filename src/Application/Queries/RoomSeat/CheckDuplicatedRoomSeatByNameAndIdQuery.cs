using MediatR;

namespace Application.Queries.RoomSeat;

public class CheckDuplicatedRoomSeatByNameAndIdQuery : IRequest<bool>
{
    public required string Name { get; set; }
    public required long Id { get; set; }
    public required long RoomId { get; set; }
}