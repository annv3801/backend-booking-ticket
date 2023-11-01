using MediatR;

namespace Application.Queries.Theater;

public class CheckDuplicatedTheaterByNameAndIdQuery : IRequest<bool>
{
    public required string Name { get; set; }
    public required long Id { get; set; }
}