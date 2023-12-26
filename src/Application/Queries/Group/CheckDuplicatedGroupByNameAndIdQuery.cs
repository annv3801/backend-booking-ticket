using MediatR;

namespace Application.Queries.Group;

public class CheckDuplicatedGroupByNameAndIdQuery : IRequest<bool>
{
    public string Title { get; set; }
    public long Id { get; set; }
}