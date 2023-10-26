using Application.Common.Models;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Category.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Category;

public interface ICategoryManagementService
{
    Task<RequestResult<bool>> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateCategoryAsync(UpdateCategoryRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteCategoryAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<CategoryResponse>> GetCategoryAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<CategoryResponse>>> GetListCategoriesAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
}