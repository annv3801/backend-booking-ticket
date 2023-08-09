using Application.Common;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations.Identity;
public class AccountLoginConfiguration : IEntityTypeConfiguration<AccountLogin>
{
    public void Configure(EntityTypeBuilder<AccountLogin> builder)
    {
        builder.HasKey(u => new {u.LoginProvider, u.ProviderKey});
        builder.HasIndex(u => new {u.LoginProvider, u.ProviderKey});

        builder.Property(u => u.LoginProvider).IsRequired().IsUnicode()
            .HasMaxLength(Constants.FieldLength.TextMaxLength);
        builder.Property(u => u.ProviderKey).IsRequired().IsUnicode()
            .HasMaxLength(Constants.FieldLength.TextMaxLength);
        builder.Property(u => u.ProviderDisplayName).IsUnicode()
            .HasMaxLength(Constants.FieldLength.DescriptionMaxLength);
        builder.ToTable("Identity_AccountLogins");
        // builder.HasOne<Account>().WithMany().HasForeignKey(table => table.AccountId);
    }
}
