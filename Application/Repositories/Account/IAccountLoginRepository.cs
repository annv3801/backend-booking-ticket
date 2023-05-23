using Application.Common.Interfaces;
using Domain.Entities.Identity;

namespace Application.Repositories.Account;
public interface IAccountLoginRepository: IRepository<AccountLogin>
{
    Task<Domain.Entities.Identity.Account?> GetAccountByAccountLogin(string email, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken));
}
