using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Theater.Requests;
using Application.DataTransferObjects.Theater.Responses;

namespace Application.Services.Theater;
public interface ITheaterManagementService
{
    Task<Result<TheaterResult>> CreateTheaterAsync(CreateTheaterRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewTheaterResponse>> ViewTheaterAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<TheaterResult>> DeleteTheaterAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<TheaterResult>> UpdateTheaterAsync(Guid id, UpdateTheaterRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewTheaterResponse>>> ViewListTheatersAsync(ViewListTheatersRequest request, CancellationToken cancellationToken = default(CancellationToken));

}
