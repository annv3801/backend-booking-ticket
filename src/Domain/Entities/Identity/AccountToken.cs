using System.Diagnostics.CodeAnalysis;
using Domain.Common.Entities;

namespace Domain.Entities.Identity;
[ExcludeFromCodeCoverage]
public class AccountToken : IEntity
{
    public long AccountId { get; set; }
    public Account? Account { get; set; }
    public string LoginProvider { get; set; }
    public string Name { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
