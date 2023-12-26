using MediatR;

namespace Application.Queries.RoomSeat;

public class CheckDuplicatedRoomSeatByNameAndIdQuery : IRequest<bool>
{
    public string Name { get; set; }
    public long Id { get; set; }
    public long RoomId { get; set; }
}