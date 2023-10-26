using Application.Common.Models.Account;

namespace Application.Common.Interfaces;
public interface ICurrentAccountService
{
    long Id { get; }
    GeneralAccountView Account { get; }
    string GetJwtToken();
}
