using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Category.Requests;
using Application.DataTransferObjects.Category.Responses;
using Application.DataTransferObjects.Pagination.Responses;

namespace Application.Services.Category;
public interface ICategoryManagementService
{
    Task<Result<CategoryResult>> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewCategoryResponse>> ViewCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<CategoryResult>> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<CategoryResult>> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewCategoryResponse>>> ViewListCategoriesAsync(ViewListCategoriesRequest request, CancellationToken cancellationToken = default(CancellationToken));

}
