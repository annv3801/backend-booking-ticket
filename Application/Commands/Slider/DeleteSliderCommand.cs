using MediatR;

namespace Application.Commands.Slider;

public class DeleteSliderCommand : IRequest<int>
{
    public DeleteSliderCommand(Domain.Entities.Slider entity)
    {
        Entity = entity;
    }

    public Domain.Entities.Slider Entity { get; set; }
}