using Application.DataTransferObjects.Scheduler.Responses;
using MediatR;

namespace Application.Queries.Scheduler;

public class GetSchedulerByDateAndTheaterIdQuery : IRequest<ICollection<SchedulerResponse>>
{
    public long TheaterId { get; set; }
    public string Date { get; set; }
}