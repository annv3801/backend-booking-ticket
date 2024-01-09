using System.Diagnostics.CodeAnalysis;
using Domain.Common.Entities;

// ReSharper disable All
#pragma warning disable 8618

namespace Domain.Entities.Identity;
[ExcludeFromCodeCoverage]
public class Permission : Entity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
    public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
}
