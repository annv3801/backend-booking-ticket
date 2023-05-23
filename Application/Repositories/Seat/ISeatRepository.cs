using Application.Common.Interfaces;
using Application.DataTransferObjects.Seat.Requests;
using Application.Queries.Seat;

namespace Application.Repositories.Seat;

public interface ISeatRepository : IRepository<Domain.Entities.Seat>
{
    Task<Domain.Entities.Seat?> GetSeatByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<Domain.Entities.Seat>> GetListSeatsAsync(ViewListSeatsRequest request, CancellationToken cancellationToken);
    Task<IQueryable<Domain.Entities.Seat>> ViewListSeatsByScheduleAsync(ViewListSeatByScheduleQuery query, CancellationToken cancellationToken = default(CancellationToken));

}