using MediatR;

namespace Application.Queries.Group;

public class CheckDuplicatedGroupByNameQuery :  IRequest<bool>
{
    public string Title { get; set; }
}