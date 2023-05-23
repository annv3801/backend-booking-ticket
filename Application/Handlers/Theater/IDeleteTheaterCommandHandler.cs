using Application.Commands.Theater;
using MediatR;

namespace Application.Handlers.Theater;

public interface IDeleteTheaterCommandHandler : IRequestHandler<DeleteTheaterCommand, int>
{
}