using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Models.Account;
[ExcludeFromCodeCoverage]
public class GeneralAccountView
{
    public Guid? Id { get; set; }
    public string? FullName { get; set; }
    public string? AvatarPhoto { get; set; }
    public string? PhoneNumber { get; set; }
}
