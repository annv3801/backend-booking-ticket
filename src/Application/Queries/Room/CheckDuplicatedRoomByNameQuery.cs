using MediatR;

namespace Application.Queries.Room;

public class CheckDuplicatedRoomByNameQuery :  IRequest<bool>
{
    public required string Name { get; set; }
}