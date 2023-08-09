using System.Diagnostics.CodeAnalysis;
using Application.Commands.Account;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using Application.Handlers.Account;
using Application.Services;
using Application.Services.Account;
using AutoMapper;

namespace Infrastructure.Handlers.Account;
[ExcludeFromCodeCoverage]
public class CreateAccountHandler : ICreateAccountHandler
{
    private readonly IMapper _mapper;
    private readonly IAccountManagementService _accountManagementService;
    private readonly IPasswordGeneratorService _passwordGeneratorService;

    public CreateAccountHandler(IMapper mapper, IAccountManagementService accountManagementService,
        IPasswordGeneratorService passwordGeneratorService)
    {
        _mapper = mapper;
        _accountManagementService = accountManagementService;
        _passwordGeneratorService = passwordGeneratorService;
    }

    public async Task<Result<CreateAccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = _mapper.Map<Domain.Entities.Identity.Account>(request);
            // Generate password
            account.PasswordHash = _passwordGeneratorService.HashPassword(request.Password);
            account.PasswordHashTemporary = account.PasswordHash;
            // End
            var result = await _accountManagementService.CreateAccountByAdminAsync(account, cancellationToken);
            return result.Succeeded
                ? Result<CreateAccountResponse>.Succeed()
                : Result<CreateAccountResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var errorItem = new ErrorItem { Error = e.Message };
            return Result<CreateAccountResponse>.Fail(new List<ErrorItem> { errorItem });
        }
    }
}
