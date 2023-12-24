using Application.DataTransferObjects.Scheduler.Responses;
using Application.DataTransferObjects.Theater.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Scheduler;
public interface ISchedulerRepository : IRepository<SchedulerEntity>
{
    Task<OffsetPaginationResponse<SchedulerResponse>> GetListSchedulersAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<OffsetPaginationResponse<SchedulerFilmAndTheaterResponse>> GetListTheaterByFilmAsync(OffsetPaginationRequest request, long filmId, CancellationToken cancellationToken);
    Task<SchedulerResponse?> GetSchedulerByIdAsync(long id, CancellationToken cancellationToken);
    Task<SchedulerEntity?> GetSchedulerEntityByIdAsync(long id, CancellationToken cancellationToken);
    Task<SchedulerResponse?> GetSchedulerByTime(long roomId, DateTimeOffset startTime, CancellationToken cancellationToken);
    Task<ICollection<SchedulerGroupResponse>> GetSchedulerByDateAndTheaterIdAsync(long theaterId, string date, CancellationToken cancellationToken);
    Task<ICollection<SchedulerGroupResponse>> GetSchedulerByDateAndTheaterIdAndFilmIdAsync(long theaterId, string date, long filmId, CancellationToken cancellationToken);
    Task<ICollection<SchedulerGroupResponse>> GetSchedulerByDateAndFilmIdAsync(string date, long filmId, CancellationToken cancellationToken);
}
