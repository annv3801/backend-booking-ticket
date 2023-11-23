using MediatR;

namespace Application.Queries.Room;

public class CheckDuplicatedRoomByNameAndIdQuery : IRequest<bool>
{
    public required long Id { get; set; }
    public required string Name { get; set; }
}