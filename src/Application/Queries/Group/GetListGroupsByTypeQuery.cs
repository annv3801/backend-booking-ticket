using Application.DataTransferObjects.Group.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Group;

public class GetListCategoriesQuery : IRequest<OffsetPaginationResponse<GroupResponse>>
{
    public string Type { get; set; }
    public required OffsetPaginationRequest OffsetPaginationRequest { get; set; }
}