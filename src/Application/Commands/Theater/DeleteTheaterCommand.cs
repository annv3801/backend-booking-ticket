using MediatR;

namespace Application.Commands.Theater;

public class DeleteTheaterCommand : IRequest<int>
{
    public required long Id { get; set; }
}