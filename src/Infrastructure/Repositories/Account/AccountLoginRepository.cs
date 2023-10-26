using Application.Interface;
using Application.Repositories.Account;
using Domain.Common.Repository;
using Domain.Entities.Identity;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Account;
public class AccountLoginRepository : Repository<AccountLogin, ApplicationDbContext>, IAccountLoginRepository
{
    private readonly DbSet<Domain.Entities.Identity.Account> _accounts;

    public AccountLoginRepository(ApplicationDbContext context, ISnowflakeIdService snowflakeIdService) : base(context, snowflakeIdService)
    {
        _accounts = context.Set<Domain.Entities.Identity.Account>();
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByAccountLogin(string email, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accounts
            .Include(u => u.AccountLogins)
            .AsSplitQuery()
            .Where(u => u.AccountLogins != null && u.AccountLogins.Any(ur =>ur.LoginProvider == loginProvider && ur.ProviderKey==providerKey && u.Email==email))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
