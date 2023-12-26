using MediatR;

namespace Application.Queries.Room;

public class CheckDuplicatedRoomByNameAndIdQuery : IRequest<bool>
{
    public long Id { get; set; }
    public string Name { get; set; }
}