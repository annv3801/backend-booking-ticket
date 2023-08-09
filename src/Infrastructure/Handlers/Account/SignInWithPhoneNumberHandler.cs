using Application.Commands.Account;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using Application.DataTransferObjects.Account.Responses;
using Application.Handlers.Account;
using Application.Services;
using Application.Services.Account;
using AutoMapper;

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

    public async Task<Result<SignInWithPhoneNumberResponse>> Handle(SignInWithPhoneNumberCommand signInWithUserNameCommand, CancellationToken cancellationToken)
    {
        try
        {
            var request = _mapper.Map<SignInWithPhoneNumberRequest>(signInWithUserNameCommand);

            var result = await _accountManagementService.SignInWithPhoneNumberAsync(request, cancellationToken);
            return result.Succeeded ? Result<SignInWithPhoneNumberResponse>.Succeed(result.Data) : Result<SignInWithPhoneNumberResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var errorItem = new ErrorItem { Error = e.Message };
            return Result<SignInWithPhoneNumberResponse>.Fail(new List<ErrorItem> { errorItem });
        }
    }
}
