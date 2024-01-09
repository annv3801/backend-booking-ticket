using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using Application.Commands.Account;
using Application.Common;
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
public class ResetOtpHandler : IResetOtpHandler
{
    private readonly IMapper _mapper;
    private readonly IAccountManagementService _accountManagementService;
    private readonly IPasswordGeneratorService _passwordGeneratorService;

    public ResetOtpHandler(IMapper mapper, IAccountManagementService accountManagementService,
        IPasswordGeneratorService passwordGeneratorService)
    {
        _mapper = mapper;
        _accountManagementService = accountManagementService;
        _passwordGeneratorService = passwordGeneratorService;
    }

    public async Task<RequestResult<AccountResult>> Handle(ResetOtpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _accountManagementService.ResetOtpAsync(cancellationToken);
            return result.Success
                ? RequestResult<AccountResult>.Succeed()
                : RequestResult<AccountResult>.Fail(result.Message, result.Errors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var errorItem = new ErrorItem { Error = e.Message };
            return RequestResult<AccountResult>.Fail("Fail", new List<ErrorItem> { errorItem });
        }
    }
}
