using System.Diagnostics.CodeAnalysis;
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
public class UpdateProfileAccountFirstLoginHandler : IUpdateProfileAccountFirstLoginHandler
{
    private readonly IMapper _mapper;
    private readonly IAccountManagementService _accountManagementService;
    private readonly IPasswordGeneratorService _passwordGeneratorService;

    public UpdateProfileAccountFirstLoginHandler(IMapper mapper, IAccountManagementService accountManagementService,
        IPasswordGeneratorService passwordGeneratorService)
    {
        _mapper = mapper;
        _accountManagementService = accountManagementService;
        _passwordGeneratorService = passwordGeneratorService;
    }

    public async Task<RequestResult<AccountResult>> Handle(UpdateProfileAccountFirstLoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = _mapper.Map<Domain.Entities.Identity.Account>(request);
            account.Status = AccountStatus.PendingConfirmation;
            return await _accountManagementService.UpdateProfileAccountFirstLoginAsync(account, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RequestResult<AccountResult>.Fail(e.Message);
        }
    }
}
