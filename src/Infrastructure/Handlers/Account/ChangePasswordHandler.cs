using System.Diagnostics.CodeAnalysis;
using Application.Commands.Account;
using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.Handlers.Account;
using Application.Services.Account;
using AutoMapper;
using Nobi.Core.Responses;

namespace Infrastructure.Handlers.Account;
[ExcludeFromCodeCoverage]
public class ChangePasswordHandler : IChangePasswordHandler
{
    private readonly IMapper _mapper;
    private readonly IAccountManagementService _accountManagementService;

    public ChangePasswordHandler(IMapper mapper, IAccountManagementService accountManagementService)
    {
        _mapper = mapper;
        _accountManagementService = accountManagementService;
    }

    public async Task<RequestResult<AccountResult>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var request = _mapper.Map<ChangePasswordRequest>(command);
            return await _accountManagementService.ChangePasswordAsync(request, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var errorItem = new ErrorItem { Error = e.Message };
            return RequestResult<AccountResult>.Fail("Fail", new List<ErrorItem> { errorItem });
        }
    }
}
