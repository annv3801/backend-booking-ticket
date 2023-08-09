using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Account.Responses;
[ExcludeFromCodeCoverage]
public class CreateAccountResponse
{
    public string GeneratedPassword { get; set; }
}
