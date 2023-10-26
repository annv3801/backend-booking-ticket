using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations.Identity;
public class AccountCategoryConfiguration : IEntityTypeConfiguration<AccountCategoryEntity>
{
    public void Configure(EntityTypeBuilder<AccountCategoryEntity> builder)
    {
        builder.HasKey(u => new {u.AccountId, u.CategoryId});
        builder.ToTable("AccountCategory", "Identity");
    }
}
