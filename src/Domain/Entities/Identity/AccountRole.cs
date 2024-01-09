using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable All

namespace Domain.Entities.Identity;
[ExcludeFromCodeCoverage]
public class AccountRole
{
    public long AccountId { get; set; }
    public Account? Account { get; set; }
    public long RoleId { get; set; }
    public Role? Role { get; set; }
}
