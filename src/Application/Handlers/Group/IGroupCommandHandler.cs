using Application.Commands.Group;
using MediatR;

namespace Application.Handlers.Group;

public interface ICreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, int>
{
}

public interface IUpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, int>
{
}

public interface IDeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, int>
{
}
