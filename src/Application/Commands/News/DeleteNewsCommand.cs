using MediatR;

namespace Application.Commands.News;

public class DeleteNewsCommand : IRequest<int>
{
    public long Id { get; set; }
}