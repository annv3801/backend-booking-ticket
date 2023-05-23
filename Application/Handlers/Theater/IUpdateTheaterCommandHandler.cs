using Application.Commands.Theater;
using MediatR;

namespace Application.Handlers.Theater;

public interface IUpdateTheaterCommandHandler : IRequestHandler<UpdateTheaterCommand, int>
{
}