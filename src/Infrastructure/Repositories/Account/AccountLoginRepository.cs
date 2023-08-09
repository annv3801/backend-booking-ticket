using Application.Repositories.Account;
using Domain.Entities.Identity;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Account;
public class AccountLoginRepository: Repository<AccountLogin>, IAccountLoginRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public AccountLoginRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByAccountLogin(string email, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Accounts
            .Include(u => u.AccountLogins)
            .AsSplitQuery()
            .Where(u => u.AccountLogins != null && u.AccountLogins.Any(ur =>ur.LoginProvider == loginProvider && ur.ProviderKey==providerKey && u.Email==email))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
