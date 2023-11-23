using Application.DataTransferObjects.Scheduler.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Scheduler;
public interface ISchedulerRepository : IRepository<SchedulerEntity>
{
    Task<OffsetPaginationResponse<SchedulerResponse>> GetListSchedulersAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<SchedulerResponse?> GetSchedulerByIdAsync(long id, CancellationToken cancellationToken);
    Task<SchedulerEntity?> GetSchedulerEntityByIdAsync(long id, CancellationToken cancellationToken);
    Task<SchedulerResponse?> GetSchedulerByTime(long roomId, DateTimeOffset startTime, CancellationToken cancellationToken);
    Task<ICollection<SchedulerResponse>> GetSchedulerByDateAndTheaterIdAsync(long theaterId, string date, CancellationToken cancellationToken);
    Task<ICollection<SchedulerResponse>> GetSchedulerByDateAndTheaterIdAndFilmIdAsync(long theaterId, string date, long filmId, CancellationToken cancellationToken);
}
