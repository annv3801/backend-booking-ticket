using MediatR;

namespace Application.Queries.Group;

public class CheckDuplicatedGroupByNameQuery :  IRequest<bool>
{
    public required string Title { get; set; }
}