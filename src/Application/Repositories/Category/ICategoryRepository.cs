using Application.Common.Interfaces;
using Application.DataTransferObjects.Category.Requests;

namespace Application.Repositories.Category;

public interface ICategoryRepository : IRepository<Domain.Entities.Category>
{
    Task<Domain.Entities.Category?> GetCategoryByShortenUrlAsync(string shortenUrl, CancellationToken cancellationToken);
    Task<Domain.Entities.Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<Domain.Entities.Category>> GetListCategoryAsync(ViewListCategoriesRequest request, CancellationToken cancellationToken);
}