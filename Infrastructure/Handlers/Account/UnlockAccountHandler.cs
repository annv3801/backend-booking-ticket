using System.Diagnostics.CodeAnalysis;
using Application.Commands.Account;
using Application.Common;
using Application.Common.Models;
using Application.Handlers.Account;
using Application.Services;
using Application.Services.Account;

namespace Infrastructure.Handlers.Account;
[ExcludeFromCodeCoverage]
public class UnlockAccountHandler : IUnlockAccountHandler
{
    private readonly IAccountManagementService _accountManagementService;

    public UnlockAccountHandler(IAccountManagementService accountManagementService)
    {
        _accountManagementService = accountManagementService;
    }

    public async Task<Result<AccountResult>> Handle(UnlockAccountCommand request,CancellationToken cancellationToken)
    {
        try
        {
            return await _accountManagementService.UnlockAccountAsync(request.Id, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var errorItem = new ErrorItem { Error = e.Message };
            return Result<AccountResult>.Fail(new List<ErrorItem> { errorItem });
        }
    }
}
