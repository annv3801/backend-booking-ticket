using System.Diagnostics.CodeAnalysis;
using Application.Commands.Account;
using Application.Common;
using Application.Common.Models;
using Application.Handlers.Account;
using Application.Services;
using Application.Services.Account;

namespace Infrastructure.Handlers.Account;
[ExcludeFromCodeCoverage]
public class LogOutHandler : ILogOutHandler
{
    private readonly IAccountManagementService _accountManagementService;

    public LogOutHandler(IAccountManagementService accountManagementService)
    {
        _accountManagementService = accountManagementService;
    }

    public async Task<Result<AccountResult>> Handle(LogOutCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _accountManagementService.LogOutAsync(command.ForceEndOtherSessions,cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var errorItem = new ErrorItem { Error = e.Message };
            return Result<AccountResult>.Fail(new List<ErrorItem> { errorItem });
        }
    }
}
