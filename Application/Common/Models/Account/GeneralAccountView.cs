using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Models.Account;
[ExcludeFromCodeCoverage]
public class GeneralAccountView
{
    public Guid? Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? AvatarPhoto { get; set; }
}
