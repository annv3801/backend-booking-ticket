using Application.DataTransferObjects.Group.Responses;
using MediatR;

namespace Application.Queries.Group;

public class GetGroupByIdQuery : IRequest<GroupResponse?>
{
    public long Id { get; set; }
}