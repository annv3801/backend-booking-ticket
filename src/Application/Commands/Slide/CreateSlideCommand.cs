using Domain.Entities;
using MediatR;

namespace Application.Commands.Slide;

public class CreateSlideCommand : IRequest<int>
{
    public SlideEntity Entity { get; set; }
}