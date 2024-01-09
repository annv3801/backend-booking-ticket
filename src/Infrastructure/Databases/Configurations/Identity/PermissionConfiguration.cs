using Application.Common;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations.Identity
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.NormalizedName);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).IsUnicode().IsRequired().HasMaxLength(Constants.FieldLength.TextMaxLength);
            builder.Property(p => p.NormalizedName).IsUnicode().IsRequired().HasMaxLength(Constants.FieldLength.TextMaxLength);
            builder.Property(p => p.Description).IsUnicode().HasMaxLength(Constants.FieldLength.DescriptionMaxLength);
            builder.Property(p => p.Code).IsUnicode().HasMaxLength(Constants.FieldLength.TextMaxLength).HasDefaultValueSql("'?'");

            builder.ToTable("Permissions", "Film");
            builder.HasData(new List<Permission>()
            {
                new Permission()
                {
                    Id = 921946681335813,
                    Description = "The system admin permission",
                    Name = "System Admin",
                    Code = Constants.Permissions.SysAdmin,
                    NormalizedName = "SYSTEM ADMIN",
                },
            });
        }
    }
}