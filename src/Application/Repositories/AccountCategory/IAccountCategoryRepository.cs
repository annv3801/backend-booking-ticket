using Application.DataTransferObjects.Category.Responses;
using Domain.Common.Pagination.OffsetBased;
using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.AccountCategory;
public interface IAccountCategoryRepository : IRepository<AccountCategoryEntity>
{
    Task<List<AccountCategoryEntity>> GetListCategoryByAccountId(long id, CancellationToken cancellationToken);
}
