using Application.Commands.Theater;
using MediatR;

namespace Application.Handlers.Theater;

public interface ICreateTheaterCommandHandler : IRequestHandler<CreateTheaterCommand, int>
{
}

public interface IUpdateTheaterCommandHandler : IRequestHandler<UpdateTheaterCommand, int>
{
}

public interface IDeleteTheaterCommandHandler : IRequestHandler<DeleteTheaterCommand, int>
{
}
