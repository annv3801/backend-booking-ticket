using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Account.Requests;
[ExcludeFromCodeCoverage]
public class ChangePasswordRequest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
    public bool ForceOtherSessionsLogout { get; set; }
    
}
