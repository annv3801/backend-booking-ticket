using Application.DataTransferObjects.Slide.Responses;
using MediatR;

namespace Application.Queries.Slide;

public class GetSlideByIdQuery : IRequest<SlideResponse?>
{
    public long Id { get; set; }
}