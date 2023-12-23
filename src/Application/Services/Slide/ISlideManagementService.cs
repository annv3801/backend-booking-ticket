using Application.Common.Models;
using Application.DataTransferObjects.Slide.Requests;
using Application.DataTransferObjects.Slide.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Slide;

public interface ISlideManagementService
{
    Task<RequestResult<bool>> CreateSlideAsync(CreateSlideRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateSlideAsync(UpdateSlideRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteSlideAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<SlideResponse>> GetSlideAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<SlideResponse>>> GetListSlidesAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
}