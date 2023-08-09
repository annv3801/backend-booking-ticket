using Application.DataTransferObjects.Slider.Requests;
using MediatR;

namespace Application.Commands.Slider;

public class CreateSliderCommand : CreateSliderRequest, IRequest<int>
{
    public CreateSliderCommand(Domain.Entities.Slider entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Slider Entity { get; set; }
}