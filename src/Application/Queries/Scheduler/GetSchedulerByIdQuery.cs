using Application.DataTransferObjects.Scheduler.Responses;
using MediatR;

namespace Application.Queries.Scheduler;

public class GetSchedulerByIdQuery : IRequest<SchedulerResponse?>
{
    public long Id { get; set; }
}