using MediatR;

namespace Application.Commands.Slide;

public class DeleteSlideCommand : IRequest<int>
{
    public required long Id { get; set; }
}