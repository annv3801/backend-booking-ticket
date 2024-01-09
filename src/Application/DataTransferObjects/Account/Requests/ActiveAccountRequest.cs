using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Application.DataTransferObjects.Account.Requests;
[ExcludeFromCodeCoverage]
public class ActiveAccountRequest
{
    public string Otp { get; set; }
}
