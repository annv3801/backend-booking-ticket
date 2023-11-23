using Application.DataTransferObjects.Room.Responses;
using MediatR;

namespace Application.Queries.Room;

public class GetRoomByIdQuery : IRequest<RoomResponse?>
{
    public long Id { get; set; }
}