using System.Diagnostics.CodeAnalysis;
using Application.Commands.Account;
using Application.Common;
using Application.Common.Models;
using Application.Handlers.Account;
using Application.Services;
using Application.Services.Account;
using AutoMapper;
using Nobi.Core.Responses;

namespace Infrastructure.Handlers.Account;
[ExcludeFromCodeCoverage]
public class UpdateAccountHandler: IUpdateAccountHandler
{
    private readonly IAccountManagementService _accountManagementService;
    private readonly IMapper _mapper;

    public UpdateAccountHandler(IAccountManagementService accountManagementService, IMapper mapper)
    {
        _accountManagementService = accountManagementService;
        _mapper = mapper;
    }

    public async Task<RequestResult<AccountResult>> Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var account = _mapper.Map<Domain.Entities.Identity.Account>(command);
            return await _accountManagementService.UpdateMyAccountAsync(account, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RequestResult<AccountResult>.Fail(e.Message);
        }
    }
}
