using Domain.Entities;
using MediatR;

namespace Application.Commands.Group;

public class CreateGroupCommand : IRequest<int>
{
    public required GroupEntity Entity { get; set; }
}