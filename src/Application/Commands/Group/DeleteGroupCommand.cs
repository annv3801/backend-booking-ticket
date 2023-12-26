using MediatR;

namespace Application.Commands.Group;

public class DeleteGroupCommand : IRequest<int>
{
    public long Id { get; set; }
}