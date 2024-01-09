using System.Diagnostics.CodeAnalysis;
using Application.Commands.Account;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using Application.Handlers.Account;
using Application.Services;
using Application.Services.Account;
using AutoMapper;
using Domain.Enums;
using Nobi.Core.Responses;

namespace Infrastructure.Handlers.Account;
[ExcludeFromCodeCoverage]
public class PreCreateAccountHandler : IPreCreateAccountHandler
{
    private readonly IMapper _mapper;
    private readonly IAccountManagementService _accountManagementService;
    private readonly IPasswordGeneratorService _passwordGeneratorService;

    public PreCreateAccountHandler(IMapper mapper, IAccountManagementService accountManagementService,
        IPasswordGeneratorService passwordGeneratorService)
    {
        _mapper = mapper;
        _accountManagementService = accountManagementService;
        _passwordGeneratorService = passwordGeneratorService;
    }

    public async Task<RequestResult<CreateAccountResponse>> Handle(PreCreateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = _mapper.Map<Domain.Entities.Identity.Account>(request);
            account.Status = AccountStatus.PendingUpdateProfile;
            // Generate password
            account.PasswordHash = _passwordGeneratorService.HashPassword(request.Password);
            account.PasswordHashTemporary = account.PasswordHash;
            // End
            var result = await _accountManagementService.PreCreateAccountByAdminAsync(account, cancellationToken);
            return result != null
                ? RequestResult<CreateAccountResponse>.Succeed()
                : RequestResult<CreateAccountResponse>.Fail("Fail", result.Errors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var errorItem = new ErrorItem { Error = e.Message };
            return RequestResult<CreateAccountResponse>.Fail("Fail", new List<ErrorItem> { errorItem });
        }
    }
}
