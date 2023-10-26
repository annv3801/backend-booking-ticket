using MediatR;

namespace Application.Commands.Group;

public class DeleteGroupCommand : IRequest<int>
{
    public required long Id { get; set; }
}