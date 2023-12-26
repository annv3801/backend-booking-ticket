using Domain.Entities;
using MediatR;

namespace Application.Commands.Group;

public class CreateGroupCommand : IRequest<int>
{
    public GroupEntity Entity { get; set; }
}