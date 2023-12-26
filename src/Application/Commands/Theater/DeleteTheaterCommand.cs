using MediatR;

namespace Application.Commands.Theater;

public class DeleteTheaterCommand : IRequest<int>
{
    public long Id { get; set; }
}