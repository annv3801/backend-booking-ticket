using MediatR;

namespace Application.Queries.Theater;

public class CheckDuplicatedTheaterByNameQuery :  IRequest<bool>
{
    public required string Name { get; set; }
}