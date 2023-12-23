using Domain.Entities;
using MediatR;

namespace Application.Commands.Slide;

public class CreateSlideCommand : IRequest<int>
{
    public required SlideEntity Entity { get; set; }
}