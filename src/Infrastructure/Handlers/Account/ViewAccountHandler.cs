using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using Application.Handlers.Account;
using Application.Queries;
using Application.Queries.Account;
using Application.Services;
using Application.Services.Account;

namespace Infrastructure.Handlers.Account;
[ExcludeFromCodeCoverage]
public class ViewAccountHandler : IViewAccountHandler
{
    /// <summary>
    /// Inject the implementation of IUserManageService (from Infrastructure project)
    /// </summary>
    private readonly IAccountManagementService _accountManagementService;

    public ViewAccountHandler(IAccountManagementService accountManagementService)
    {
        _accountManagementService = accountManagementService;
    }
    
    public async Task<Result<ViewAccountResponse>> Handle(ViewAccountQuery request, CancellationToken cancellationToken)
    {
        return  await _accountManagementService.ViewAccountDetailByAdminAsync(request.AccountId, cancellationToken);
    }
}
