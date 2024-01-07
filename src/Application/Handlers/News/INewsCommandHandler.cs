using Application.Commands.News;
using MediatR;

namespace Application.Handlers.News;

public interface ICreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, int>
{
}

public interface IUpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, int>
{
}

public interface IDeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, int>
{
}
