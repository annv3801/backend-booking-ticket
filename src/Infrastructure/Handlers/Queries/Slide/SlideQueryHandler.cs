using Application.DataTransferObjects.Slide.Responses;
using Application.Queries.Slide;
using Application.Repositories.Slide;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.Slide;

public class SlideQueryHandler :
    IRequestHandler<GetSlideByIdQuery, SlideResponse?>, 
    IRequestHandler<GetListSlidesQuery, OffsetPaginationResponse<SlideResponse>>
{
    private readonly ISlideRepository _slideRepository;

    public SlideQueryHandler(ISlideRepository slideReadOnlyRepository)
    {
        _slideRepository = slideReadOnlyRepository;
    }

    public async Task<SlideResponse?> Handle(GetSlideByIdQuery request, CancellationToken cancellationToken)
    {
        return await _slideRepository.GetSlideByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<SlideResponse>> Handle(GetListSlidesQuery request, CancellationToken cancellationToken)
    {
        return await _slideRepository.GetListSlidesAsync(request.OffsetPaginationRequest, cancellationToken);
    }
}