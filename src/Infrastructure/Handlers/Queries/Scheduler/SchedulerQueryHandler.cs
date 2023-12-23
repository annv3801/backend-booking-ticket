using Application.DataTransferObjects.Scheduler.Responses;
using Application.Queries.Scheduler;
using Application.Repositories.Scheduler;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Infrastructure.Handlers.Queries.Scheduler;

public class SchedulerQueryHandler :
    IRequestHandler<GetSchedulerByIdQuery, SchedulerResponse?>, 
    IRequestHandler<GetListSchedulersQuery, OffsetPaginationResponse<SchedulerResponse>>,
    IRequestHandler<GetSchedulerByDateAndTheaterIdQuery, ICollection<SchedulerGroupResponse>>
{
    private readonly ISchedulerRepository _schedulerRepository;

    public SchedulerQueryHandler(ISchedulerRepository schedulerRepository)
    {
        _schedulerRepository = schedulerRepository;
    }

    public async Task<SchedulerResponse?> Handle(GetSchedulerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _schedulerRepository.GetSchedulerByIdAsync(request.Id, cancellationToken);
    }
    
    public async Task<OffsetPaginationResponse<SchedulerResponse>> Handle(GetListSchedulersQuery request, CancellationToken cancellationToken)
    {
        return await _schedulerRepository.GetListSchedulersAsync(request.OffsetPaginationRequest, cancellationToken);
    }
    
    public async Task<ICollection<SchedulerGroupResponse>> Handle(GetSchedulerByDateAndTheaterIdQuery request, CancellationToken cancellationToken)
    {
        return await _schedulerRepository.GetSchedulerByDateAndTheaterIdAsync(request.TheaterId, request.Date, cancellationToken);
    }
    
    public async Task<ICollection<SchedulerGroupResponse>> Handle(GetSchedulerByDateAndFilmIdQuery request, CancellationToken cancellationToken)
    {
        return await _schedulerRepository.GetSchedulerByDateAndTheaterIdAsync(request.FilmId, request.Date, cancellationToken);
    }
}