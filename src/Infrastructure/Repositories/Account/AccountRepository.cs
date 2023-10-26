using Application.Interface;
using Application.Repositories.Account;
using Domain.Common.Repository;
using Domain.Enums;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Account;
public class AccountRepository : Repository<Domain.Entities.Identity.Account, ApplicationDbContext>, IAccountRepository
{
    private readonly DbSet<Domain.Entities.Identity.Account> _accounts;
    private readonly DbSet<Domain.Entities.Identity.AccountToken> _accountsTokens;

    public AccountRepository(ApplicationDbContext context, ISnowflakeIdService snowflakeIdService) : base(context, snowflakeIdService)
    {
        _accounts = context.Set<Domain.Entities.Identity.Account>();
        _accountsTokens = context.Set<Domain.Entities.Identity.AccountToken>();
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accounts
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && u.Status != AccountStatus.Inactive, cancellationToken);
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByUserNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accounts
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.UserName == userName && u.Status != AccountStatus.Deleted, cancellationToken);

    }

    public async Task<bool> IsDuplicatedEmailAsync(long accountId, string email, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accounts.AnyAsync(u => u.NormalizedEmail == email.ToUpper() && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
    }
    
    public async Task<bool> IsDuplicatedUserNameAsync(long accountId, string userName, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accounts.AnyAsync(u => u.NormalizedUserName == userName.ToUpper() && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
    }

    public async Task<bool> IsDuplicatedPhoneNumberAsync(long accountId, string? phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accounts.AnyAsync(u => u.PhoneNumber == phoneNumber && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accounts.FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper() || u.Email == email.ToUpper(), cancellationToken);
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByEmailForCheckDuplicateAsync(long accountId, string email, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accounts.FirstOrDefaultAsync(u => (u.NormalizedEmail == email.ToUpper() || u.Email == email.ToUpper()) && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByPhoneForCheckDuplicateAsync(long accountId, string phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accounts.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
    }
    public async Task<bool> IsValidJwtAsync(long accountId, string jwt, string loginProvider, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            return await _accountsTokens.AnyAsync(t => t.AccountId == accountId && t.Token == jwt && t.LoginProvider == loginProvider, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Domain.Entities.Identity.Account?> ViewAccountDetailByAdmin(long userId, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accounts
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Identity.Account>> ViewListAccountsByAdminAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _accounts
            .AsSplitQuery()
            .AsQueryable();
    }

    public async Task<Domain.Entities.Identity.Account?> ViewMyAccountAsync(long userId, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _accounts
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public Task<Domain.Entities.Identity.Account> RegisterAccountAsync(string role, CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }
}
