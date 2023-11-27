using Application.Common;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations.Identity;
public class AccountTokenConfiguration : IEntityTypeConfiguration<AccountToken>
{
    public void Configure(EntityTypeBuilder<AccountToken> builder)
    {
        builder.HasKey(u => new {u.LoginProvider, u.Name, u.AccountId});
        builder.HasIndex(u => new {u.LoginProvider, u.Name, u.AccountId});
        builder.Property(u => u.LoginProvider).IsRequired().IsUnicode()
            .HasMaxLength(Constants.FieldLength.TextMaxLength);
        builder.Property(u => u.Name).IsRequired().IsUnicode().HasMaxLength(Constants.FieldLength.TextMaxLength);
        builder.ToTable("AccountTokens", "Film");
    }
}
