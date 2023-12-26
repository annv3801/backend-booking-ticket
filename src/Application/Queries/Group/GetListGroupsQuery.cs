using Application.DataTransferObjects.Group.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Group;

public class GetListGroupsQuery : IRequest<OffsetPaginationResponse<GroupResponse>>
{
    public OffsetPaginationRequest OffsetPaginationRequest { get; set; }
}