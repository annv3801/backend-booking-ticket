using Application.Commands.News;
using MediatR;

namespace Application.Handlers.News;

public interface ICreateNewsCommandHandler: IRequestHandler<CreateNewsCommand, int>
{
    
}