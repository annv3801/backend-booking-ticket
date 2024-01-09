using System.Diagnostics.CodeAnalysis;
using Domain.Common.Entities;
using Domain.Enums;

// ReSharper disable All
#pragma warning disable 8618

namespace Domain.Entities.Identity;
[ExcludeFromCodeCoverage]
public class Role : Entity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public RoleStatus Status { get; set; }
    public string? Description { get; set; }
    public ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
