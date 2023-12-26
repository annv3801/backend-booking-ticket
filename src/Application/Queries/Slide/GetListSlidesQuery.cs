using Application.DataTransferObjects.Slide.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Slide;

public class GetListSlidesQuery : IRequest<OffsetPaginationResponse<SlideResponse>>
{
    public OffsetPaginationRequest OffsetPaginationRequest { get; set; }
}