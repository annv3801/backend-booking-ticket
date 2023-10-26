using Application.Interface;
using Application.Repositories.Account;
using Domain.Common.Repository;
using Domain.Entities.Identity;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Account;
public class AccountTokenRepository : Repository<AccountToken, ApplicationDbContext>, IAccountTokenRepository
{
    private readonly DbSet<AccountToken> _accountsTokens;

    public AccountTokenRepository(ApplicationDbContext context, ISnowflakeIdService snowflakeIdService) : base(context, snowflakeIdService)
    {
        _accountsTokens = context.Set<AccountToken>();
    }

    public async Task<List<AccountToken>> GetAccountTokensByAccountAsync(long accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accountsTokens
            .Include(at => at.Account)
            .AsSplitQuery()
            .Where(at => at.AccountId == accountId).ToListAsync(cancellationToken);
    }

    public async Task<AccountToken> GetAccountTokensByTokenAsync(string token, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accountsTokens.FirstOrDefaultAsync(t=>t.Token == token,cancellationToken);
    }
}
