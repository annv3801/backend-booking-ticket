using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Category.Responses;
using Application.DataTransferObjects.Pagination.Requests;
using Application.DataTransferObjects.Pagination.Responses;
using MediatR;

namespace Application.Queries.Category;
[ExcludeFromCodeCoverage]
public class ViewListCategoriesQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewCategoryResponse>>>
{
}
