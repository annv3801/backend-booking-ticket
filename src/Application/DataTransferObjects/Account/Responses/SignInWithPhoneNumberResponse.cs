using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Account.Responses;
[ExcludeFromCodeCoverage]
public class SignInWithPhoneNumberResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
