using Application.Common.Interfaces;

namespace Application.Repositories.Account;
public interface IAccountRepository : IRepository<Domain.Entities.Identity.Account>
{
    Task<Domain.Entities.Identity.Account?> GetAccountByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> IsDuplicatedEmailAsync(Guid accountId, string email, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> IsDuplicatedPhoneNumberAsync(Guid accountId, string? phone, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Account?> ViewAccountDetailByAdmin(Guid userId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Account?> ViewMyAccountAsync(Guid userId, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> IsValidJwtAsync(Guid accountId, string jwt, string loginProvider, CancellationToken cancellationToken = default(CancellationToken));
}
