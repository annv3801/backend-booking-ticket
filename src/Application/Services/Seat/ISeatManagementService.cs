using Application.DataTransferObjects.Seat.Requests;
using Application.DataTransferObjects.Seat.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Seat;

public interface ISeatManagementService
{
    Task<RequestResult<bool>> CreateSeatAsync(CreateSeatRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateSeatAsync(UpdateSeatRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteSeatAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<SeatResponse>> GetSeatAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<SeatResponse>>> GetListSeatsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
}