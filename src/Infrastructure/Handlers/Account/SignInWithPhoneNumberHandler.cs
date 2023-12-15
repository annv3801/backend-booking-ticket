using Application.Commands.Account;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Account.Responses;
using Application.Handlers.Account;
using Application.Services;
using Application.Services.Account;
using AutoMapper;
using Nobi.Core.Responses;

namespace Infrastructure.Handlers.Account;
public class SignInWithPhoneNumberHandler : ISignInWithPhoneNumberHandler
{
    private readonly IMapper _mapper;
    private readonly IAccountManagementService _accountManagementService;

    public SignInWithPhoneNumberHandler(IAccountManagementService accountManagementService, IMapper mapper)
    {
        _accountManagementService = accountManagementService;
        _mapper = mapper;
    }

    public async Task<RequestResult<SignInWithPhoneNumberResponse>> Handle(SignInWithPhoneNumberCommand signInWithUserNameCommand, CancellationToken cancellationToken)
    {
        try
        {
            var request = _mapper.Map<SignInWithPhoneNumberRequest>(signInWithUserNameCommand);

            var result = await _accountManagementService.SignInWithPhoneNumberAsync(request, cancellationToken);
            if (result.Success)
                return RequestResult<SignInWithPhoneNumberResponse>.Succeed("Success", result.Data);
            return RequestResult<SignInWithPhoneNumberResponse>.Fail(result.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var errorItem = new ErrorItem { Error = e.Message };
            return RequestResult<SignInWithPhoneNumberResponse>.Fail("Fail", new List<ErrorItem> { errorItem });
        }
    }
}
