using Application.DataTransferObjects.Theater.Requests;
using Application.DataTransferObjects.Theater.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Theater;

public interface ITheaterManagementService
{
    Task<RequestResult<bool>> CreateTheaterAsync(CreateTheaterRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateTheaterAsync(UpdateTheaterRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteTheaterAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<TheaterResponse>> GetTheaterAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<TheaterResponse>>> GetListTheatersAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<TheaterResponse>>> GetListTheatersFavoritesAsync(ViewTheaterFavoriteRequest request, CancellationToken cancellationToken);
}