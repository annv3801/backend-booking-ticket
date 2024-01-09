using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#pragma warning disable 8602
namespace Infrastructure.Databases.Configurations.Identity
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(u => new {u.PermissionId, u.RoleId});
            builder.HasIndex(u => new {u.PermissionId, u.RoleId});
            builder.ToTable("RolePermissions", "Film");
            builder.HasOne(r => r.Role).WithMany(r => r.RolePermissions).HasForeignKey(r => r.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Permission).WithMany(p => p.RolePermissions).HasForeignKey(p => p.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new List<RolePermission>()
            {
                new RolePermission()
                {
                    RoleId = 921946681335812,
                    PermissionId = 921946681335813
                }
            });
        }
    }
}