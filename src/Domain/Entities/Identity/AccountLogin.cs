using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.Identity;
[ExcludeFromCodeCoverage]
public class AccountLogin
{
    public Guid AccountId { get; set; }
    public Account? Account { get; set; }
    public string LoginProvider { get; set; }
    public string ProviderKey { get; set; }
    public string ProviderDisplayName { get; set; }
}
