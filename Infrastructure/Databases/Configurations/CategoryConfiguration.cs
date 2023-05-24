using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasIndex(r => r.Name);
        builder.HasIndex(r => r.ShortenUrl);
        builder.HasIndex(r => r.Status);
        builder.ToTable("Categories");
    }
}
