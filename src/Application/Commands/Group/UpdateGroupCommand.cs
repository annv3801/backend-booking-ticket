using Domain.Entities;
using MediatR;

namespace Application.Commands.Group;

public class UpdateGroupCommand : IRequest<int>
{
    public GroupEntity Request { get; set; }
}