using Application.DataTransferObjects.Category.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.Category;
public interface ICategoryRepository : IRepository<Domain.Entities.CategoryEntity>
{
    Task<OffsetPaginationResponse<CategoryResponse>> GetListCategoriesAsync(OffsetPaginationRequest request, CancellationToken cancellationToken);
    Task<CategoryResponse?> GetCategoryByIdAsync(long id, CancellationToken cancellationToken);
    Task<CategoryEntity?> GetCategoryEntityByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedCategoryByNameAndIdAsync(string name, long id, CancellationToken cancellationToken);
    Task<bool> IsDuplicatedCategoryByNameAsync(string name, CancellationToken cancellationToken);
}
