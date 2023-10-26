using Application.Common.Interfaces;
using Domain.Common.Repository;
using Domain.Entities.Identity;

namespace Application.Repositories.Account;
public interface IAccountTokenRepository : IRepository<AccountToken>
{
    Task<List<AccountToken>> GetAccountTokensByAccountAsync(long accountId, CancellationToken cancellationToken = default(CancellationToken));
    Task<AccountToken> GetAccountTokensByTokenAsync(string token, CancellationToken cancellationToken = default(CancellationToken));
    
}
