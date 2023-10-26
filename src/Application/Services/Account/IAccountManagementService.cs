using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Account.Responses;
using Nobi.Core.Responses;

namespace Application.Services.Account;
public interface IAccountManagementService
{
    Task<RequestResult<AccountResult>> CreateAccountByAdminAsync(Domain.Entities.Identity.Account account,CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<AccountResult>> UpdateAccountAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<ViewAccountResponse>> ViewAccountDetailByAdminAsync(long accountId, CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<AccountResult>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<SignInWithPhoneNumberResponse>> SignInWithPhoneNumberAsync(SignInWithPhoneNumberRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<AccountResult>> LogOutAsync(bool forceEndOtherSessions=false,CancellationToken cancellationToken = default(CancellationToken));
}
