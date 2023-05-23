using Application.Commands.Theater;
using MediatR;

namespace Application.Handlers.Theater;

public interface ICreateTheaterCommandHandler: IRequestHandler<CreateTheaterCommand, int>
{
    
}