using Domain.Entities;
using MediatR;

namespace Application.Commands.Slide;

public class UpdateSlideCommand : IRequest<int>
{
    public required SlideEntity Request { get; set; }
}