using Domain.Common.Repository;
using Domain.Entities;

namespace Application.Repositories.AccountFavorite;
public interface IAccountFavoriteRepository : IRepository<AccountFavoritesEntity>
{
    Task<List<AccountFavoritesEntity>> GetListFavoriteByAccountId(long id, CancellationToken cancellationToken);
    Task<AccountFavoritesEntity> IsHaveFavorite(long accountId, long? filmId, long? theaterId, CancellationToken cancellationToken);
}
