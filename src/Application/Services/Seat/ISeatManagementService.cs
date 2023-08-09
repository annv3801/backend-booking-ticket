using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Pagination.Responses;
using Application.DataTransferObjects.Seat.Requests;
using Application.DataTransferObjects.Seat.Responses;
using Application.Queries.Seat;

namespace Application.Services.Seat;
public interface ISeatManagementService
{
    Task<Result<SeatResult>> CreateSeatAsync(CreateSeatRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewSeatResponse>> ViewSeatAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<SeatResult>> DeleteSeatAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewSeatResponse>>> ViewListSeatsAsync(ViewListSeatsRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewSeatResponse>>> ViewListSeatsByScheduleAsync(ViewListSeatsByScheduleRequest query, CancellationToken cancellationToken = default(CancellationToken));
}
