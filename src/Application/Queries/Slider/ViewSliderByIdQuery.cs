using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Slider.Responses;
using MediatR;

namespace Application.Queries.Slider;
[ExcludeFromCodeCoverage]
public class ViewSliderByIdQuery : IRequest<Result<ViewSliderResponse>>
{
    public ViewSliderByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
