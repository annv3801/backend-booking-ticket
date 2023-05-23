using Application.Commands.News;
using MediatR;

namespace Application.Handlers.News;

public interface IDeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, int>
{
}