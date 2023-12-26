using Application.DataTransferObjects.Category.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;

namespace Application.Queries.Category;

public class GetListCategoriesQuery : IRequest<OffsetPaginationResponse<CategoryResponse>>
{
    public OffsetPaginationRequest OffsetPaginationRequest { get; set; }
}