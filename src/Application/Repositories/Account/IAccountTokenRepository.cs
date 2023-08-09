using Application.Common.Interfaces;
using Domain.Entities.Identity;

namespace Application.Repositories.Account;
public interface IAccountTokenRepository : IRepository<AccountToken>
{
    Task<List<AccountToken>> GetAccountTokensByAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken));
    Task<AccountToken> GetAccountTokensByTokenAsync(string token, CancellationToken cancellationToken = default(CancellationToken));
    
}
