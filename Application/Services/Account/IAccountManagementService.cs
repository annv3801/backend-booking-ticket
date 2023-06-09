using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Account.Responses;

namespace Application.Services.Account;
public interface IAccountManagementService
{
    Task<Result<AccountResult>> CreateAccountByAdminAsync(Domain.Entities.Identity.Account account,CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> UpdateAccountAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewAccountResponse>> ViewAccountDetailByAdminAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> UnlockAccountAsync(Guid accountId,CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<SignInWithPhoneNumberResponse>> SignInWithPhoneNumberAsync(SignInWithPhoneNumberRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> LogOutAsync(bool forceEndOtherSessions=false,CancellationToken cancellationToken = default(CancellationToken));
}
