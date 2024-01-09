using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#pragma warning disable 8602
namespace Infrastructure.Databases.Configurations.Identity
{
    public class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole>
    {
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            builder.HasKey(u => new {u.AccountId, u.RoleId});
            builder.HasIndex(u => new {u.AccountId, u.RoleId});
            builder.ToTable("AccountRoles", "Film");
            builder.HasOne(r => r.Role).WithMany(r => r.AccountRoles).HasForeignKey(r => r.RoleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.Account).WithMany(u => u.AccountRoles).HasForeignKey(u => u.AccountId).OnDelete(DeleteBehavior.Cascade);
            builder.HasData(new List<AccountRole>()
            {
                new AccountRole()
                {
                    RoleId = 921946681335812,
                    AccountId = 921946681335811
                },
            });
        }
    }
}