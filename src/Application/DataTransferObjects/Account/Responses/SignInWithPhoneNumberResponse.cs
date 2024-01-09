using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Application.DataTransferObjects.Account.Responses;
[ExcludeFromCodeCoverage]
public class SignInWithPhoneNumberResponse
{
    public ProfileAccount Profile { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class ProfileAccount
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Avatar { get; set; }
    public bool Gender { get; set; }
    public string? FullName { get;set; }
    public AccountStatus Status { get; set; }
}
