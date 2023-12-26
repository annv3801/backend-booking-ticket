using MediatR;

namespace Application.Commands.Slide;

public class DeleteSlideCommand : IRequest<int>
{
    public long Id { get; set; }
}