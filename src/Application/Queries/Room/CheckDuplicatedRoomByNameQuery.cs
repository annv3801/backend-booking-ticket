using MediatR;

namespace Application.Queries.Room;

public class CheckDuplicatedRoomByNameQuery :  IRequest<bool>
{
    public string Name { get; set; }
}