using Application.DataTransferObjects.Scheduler.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Scheduler;

public class GetListSchedulersQuery : IRequest<OffsetPaginationResponse<SchedulerResponse>>
{
    public required OffsetPaginationRequest OffsetPaginationRequest { get; set; }
}