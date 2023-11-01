using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations.Identity;
public class AccountFavoritesConfiguration : IEntityTypeConfiguration<AccountFavoritesEntity>
{
    public void Configure(EntityTypeBuilder<AccountFavoritesEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.ToTable("AccountFavorites", "Identity");
    }
}
