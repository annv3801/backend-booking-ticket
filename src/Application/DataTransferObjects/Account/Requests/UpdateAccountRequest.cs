using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Application.DataTransferObjects.Account.Requests;
[ExcludeFromCodeCoverage]
public class UpdateAccountRequest
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public IFormFile? AvatarPhoto { get; set; }
    public bool? Gender { get; set; }
}
