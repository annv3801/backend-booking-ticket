using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Slider.Requests;
using Application.DataTransferObjects.Slider.Responses;

namespace Application.Services.Slider;
public interface ISliderManagementService
{
    Task<Result<SliderResult>> CreateSliderAsync(CreateSliderRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewSliderResponse>> ViewSliderAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewSliderResponse>>> ViewListSlidersAsync(ViewListSlidersRequest request, CancellationToken cancellationToken = default(CancellationToken));

}