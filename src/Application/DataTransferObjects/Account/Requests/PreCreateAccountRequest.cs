using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Application.DataTransferObjects.Account.Requests;
[ExcludeFromCodeCoverage]
public class PreCreateAccountRequest
{
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
}
