using Application.DataTransferObjects.Scheduler.Responses;
using Application.DataTransferObjects.Theater.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Scheduler;

public class GetTheatersByFilmQuery : IRequest<OffsetPaginationResponse<SchedulerFilmAndTheaterResponse>>
{
    public OffsetPaginationRequest OffsetPaginationRequest { get; set; }
    public long FilmId { get; set; }
}