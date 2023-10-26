using MediatR;

namespace Application.Queries.Group;

public class CheckDuplicatedGroupByNameAndIdQuery : IRequest<bool>
{
    public required string Title { get; set; }
    public required long Id { get; set; }
}