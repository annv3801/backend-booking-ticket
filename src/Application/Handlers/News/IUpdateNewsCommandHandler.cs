using Application.Commands.News;
using MediatR;

namespace Application.Handlers.News;

public interface IUpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, int>
{
}