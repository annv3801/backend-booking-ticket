using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Configurations;
[ExcludeFromCodeCoverage]
public class JwtConfiguration
{
    public string Audience { get; set; } = "localhost";
    public string Issuer { get; set; } = "localhost";
    public string SymmetricSecurityKey { get; set; } = "ymF#R80S6;XHg.[*g9E+O>-Y%qzR&8sw<z5M$L#J0R*gr3Ud#7A=>7w:^EUf_=,";

    public int Expires { get; set; } = 2400;
    public int RefreshTokenExpires { get; set; } = 2400;
}
