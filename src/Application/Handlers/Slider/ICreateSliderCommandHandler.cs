using Application.Commands.Slider;
using MediatR;

namespace Application.Handlers.Slider;

public interface ICreateSliderCommandHandler: IRequestHandler<CreateSliderCommand, int>
{
    
}