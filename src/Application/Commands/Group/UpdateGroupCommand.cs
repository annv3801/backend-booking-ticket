using Domain.Entities;
using MediatR;

namespace Application.Commands.Group;

public class UpdateGroupCommand : IRequest<int>
{
    public required GroupEntity Request { get; set; }
}