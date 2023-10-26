using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Account.Requests;
[ExcludeFromCodeCoverage]
public class CreateAccountRequest
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AvatarPhoto { get; set; }
    public bool? Gender { get; set; }
    public string? Password { get; set; }
}
