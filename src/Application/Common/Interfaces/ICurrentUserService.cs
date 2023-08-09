using Application.Common.Models.Account;

namespace Application.Common.Interfaces;
public interface ICurrentAccountService
{
    Guid Id { get; }
    GeneralAccountView Account { get; }
    string GetJwtToken();
}
