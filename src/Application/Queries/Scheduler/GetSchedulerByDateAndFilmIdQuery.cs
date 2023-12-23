using Application.DataTransferObjects.Scheduler.Responses;
using MediatR;

namespace Application.Queries.Scheduler;

public class GetSchedulerByDateAndFilmIdQuery : IRequest<ICollection<SchedulerGroupResponse>>
{
    public long FilmId { get; set; }
    public string Date { get; set; }
}