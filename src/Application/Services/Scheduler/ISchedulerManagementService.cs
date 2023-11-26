using Application.DataTransferObjects.Scheduler.Requests;
using Application.DataTransferObjects.Scheduler.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Scheduler;

public interface ISchedulerManagementService
{
    Task<RequestResult<bool>> CreateSchedulerAsync(CreateSchedulerRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateSchedulerAsync(UpdateSchedulerRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteSchedulerAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<SchedulerResponse>> GetSchedulerAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<ICollection<SchedulerResponse>>> GetSchedulerByTheaterIdAndDateAsync(long theaterId, string date, CancellationToken cancellationToken);
    Task<RequestResult<ICollection<SchedulerResponse>>> GetSchedulerByTheaterIdAndDateAndFilmIdAsync(long theaterId, string date, long filmId, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<SchedulerResponse>>> GetListSchedulersAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
}