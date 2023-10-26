using Application.Common.Interfaces;
using Domain.Common.Repository;

namespace Application.Repositories.Account;
public interface IAccountRepository : IRepository<Domain.Entities.Identity.Account>
{
    Task<Domain.Entities.Identity.Account?> GetAccountByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> IsDuplicatedEmailAsync(long accountId, string email, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> IsDuplicatedUserNameAsync(long accountId, string userName, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> IsDuplicatedPhoneNumberAsync(long accountId, string? phone, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Account?> ViewAccountDetailByAdmin(long userId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Account?> ViewMyAccountAsync(long userId, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> IsValidJwtAsync(long accountId, string jwt, string loginProvider, CancellationToken cancellationToken = default(CancellationToken));
}
