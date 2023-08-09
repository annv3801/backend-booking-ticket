using System.Diagnostics.CodeAnalysis;

namespace Application.DataTransferObjects.Jwt.Responses;
[ExcludeFromCodeCoverage]
public class CreateJwtResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
