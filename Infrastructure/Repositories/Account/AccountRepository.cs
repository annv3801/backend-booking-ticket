using Application.Repositories.Account;
using Domain.Enums;
using Infrastructure.Common.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Account;
public class AccountRepository : Repository<Domain.Entities.Identity.Account>, IAccountRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AccountRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Accounts
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && u.Status != AccountStatus.Inactive, cancellationToken);
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByUserNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Accounts
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.UserName == userName && u.Status != AccountStatus.Deleted, cancellationToken);

    }

    public async Task<bool> IsDuplicatedEmailAsync(Guid accountId, string email, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Accounts.AnyAsync(u => u.NormalizedEmail == email.ToUpper() && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
    }

    public async Task<bool> IsDuplicatedPhoneNumberAsync(Guid accountId, string? phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Accounts.AnyAsync(u => u.PhoneNumber == phoneNumber && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Accounts.FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper() || u.Email == email.ToUpper(), cancellationToken);
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByEmailForCheckDuplicateAsync(Guid accountId, string email, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Accounts.FirstOrDefaultAsync(u => (u.NormalizedEmail == email.ToUpper() || u.Email == email.ToUpper()) && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByPhoneForCheckDuplicateAsync(Guid accountId, string phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Accounts.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
    }
    public async Task<bool> IsValidJwtAsync(Guid accountId, string jwt, string loginProvider, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            return await _applicationDbContext.AccountTokens.AnyAsync(t => t.AccountId == accountId && t.Token == jwt && t.LoginProvider == loginProvider, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Domain.Entities.Identity.Account?> ViewAccountDetailByAdmin(Guid userId, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Accounts
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Identity.Account>> ViewListAccountsByAdminAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.Accounts
            .AsSplitQuery()
            .AsQueryable();
    }

    public async Task<Domain.Entities.Identity.Account?> ViewMyAccountAsync(Guid userId, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Accounts
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public Task<Domain.Entities.Identity.Account> RegisterAccountAsync(string role, CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }
}
