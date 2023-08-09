using Application.Commands.Slider;
using MediatR;

namespace Application.Handlers.Slider;

public interface IDeleteSliderCommandHandler : IRequestHandler<DeleteSliderCommand, int>
{
}