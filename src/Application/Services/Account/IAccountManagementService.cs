using Application.Commands.Account;
using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Account.Responses;
using Nobi.Core.Responses;

namespace Application.Services.Account;
public interface IAccountManagementService
{
    Task<RequestResult<AccountResult>> CreateAccountByAdminAsync(Domain.Entities.Identity.Account account,CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<AccountResult>> PreCreateAccountByAdminAsync(Domain.Entities.Identity.Account account,CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<AccountResult>> UpdateProfileAccountFirstLoginAsync(Domain.Entities.Identity.Account account,CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<AccountResult>> UpdateAccountAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<AccountResult>> UpdateMyAccountAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<AccountResult>> ActiveAccountAsync(string otp, CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<AccountResult>> ResetOtpAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<ViewAccountResponse>> ViewAccountDetailByAdminAsync(long accountId, CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<AccountResult>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<SignInWithPhoneNumberResponse>> SignInWithPhoneNumberAsync(SignInWithPhoneNumberRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<AccountResult>> LogOutAsync(bool forceEndOtherSessions=false,CancellationToken cancellationToken = default(CancellationToken));
    Task<RequestResult<bool>> CreateAndUpdateAccountCategoryAsync(CreateAndUpdateAccountCategoryRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> CreateAndUpdateAccountFavoriteAsync(CreateAndUpdateAccountFavoriteRequest request, CancellationToken cancellationToken);
}
