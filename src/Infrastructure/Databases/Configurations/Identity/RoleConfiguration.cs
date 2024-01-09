using Domain.Entities.Identity;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.Name);
            builder.HasIndex(r => r.NormalizedName);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.ToTable("Roles", "Film");
            builder.HasData(new List<Role>()
            {
                new Role()
                {
                    Id = 921946681335812,
                    Description = "The system Admin Role",
                    Name = "System Admin",
                    Status = RoleStatus.Active,
                    NormalizedName = "SYSTEM ADMIN",
                }
            });
        }
    }
}