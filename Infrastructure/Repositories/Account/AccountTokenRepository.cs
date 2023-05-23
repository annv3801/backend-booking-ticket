using Application.Repositories.Account;
using Domain.Entities.Identity;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Account;
public class AccountTokenRepository : Repository<AccountToken>, IAccountTokenRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AccountTokenRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<AccountToken>> GetAccountTokensByAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.AccountTokens
            .Include(at => at.Account)
            .AsSplitQuery()
            .Where(at => at.AccountId == accountId).ToListAsync(cancellationToken);
    }

    public async Task<AccountToken> GetAccountTokensByTokenAsync(string token, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.AccountTokens.FirstOrDefaultAsync(t=>t.Token==token,cancellationToken);
    }
}
