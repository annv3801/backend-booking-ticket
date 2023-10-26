using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Account.Requests;
[ExcludeFromCodeCoverage]
public class UpdateAccountRequest
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string? AvatarPhoto { get; set; }
    public bool? Gender { get; set; }
}
