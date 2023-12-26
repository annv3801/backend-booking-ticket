using MediatR;

namespace Application.Queries.Theater;

public class CheckDuplicatedTheaterByNameAndIdQuery : IRequest<bool>
{
    public string Name { get; set; }
    public long Id { get; set; }
}