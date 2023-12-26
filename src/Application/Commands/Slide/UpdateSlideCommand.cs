using Domain.Entities;
using MediatR;

namespace Application.Commands.Slide;

public class UpdateSlideCommand : IRequest<int>
{
    public SlideEntity Request { get; set; }
}