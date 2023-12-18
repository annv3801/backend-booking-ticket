using System.Collections;
using Application.DataTransferObjects.Seat.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Seat;
public interface ISeatRepository : IRepository<SeatEntity>
{
    Task<OffsetPaginationResponse<SeatResponse>> GetListSeatsAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<ICollection<SeatResponse>> GetListSeatsBySchedulerAsync(long schedulerId, CancellationToken cancellationToken);
    Task<SeatResponse?> GetSeatByIdAsync(long id, CancellationToken cancellationToken);
    Task<SeatEntity?> GetSeatEntityByIdAsync(long id, CancellationToken cancellationToken);
}
