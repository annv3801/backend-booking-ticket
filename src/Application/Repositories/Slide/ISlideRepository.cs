using Application.DataTransferObjects.Slide.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Slide;
public interface ISlideRepository : IRepository<Domain.Entities.SlideEntity>
{
    Task<OffsetPaginationResponse<SlideResponse>> GetListSlidesAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<SlideResponse?> GetSlideByIdAsync(long id, CancellationToken cancellationToken);
    Task<SlideEntity?> GetSlideEntityByIdAsync(long id, CancellationToken cancellationToken);
}
