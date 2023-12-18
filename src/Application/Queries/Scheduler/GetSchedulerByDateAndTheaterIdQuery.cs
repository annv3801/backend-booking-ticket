using Application.DataTransferObjects.Scheduler.Responses;
using MediatR;

namespace Application.Queries.Scheduler;

public class GetSchedulerByDateAndTheaterIdQuery : IRequest<ICollection<SchedulerGroupResponse>>
{
    public long TheaterId { get; set; }
    public string Date { get; set; }
}