using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations;

public class FilmEntityConfiguration : IEntityTypeConfiguration<FilmEntity>
{
    public void Configure(EntityTypeBuilder<FilmEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.HasIndex(r => r.Name).IsUnique();
        builder.Property(r => r.CategoryIds).IsUnicode().HasColumnType("jsonb");
        builder.ToTable("Films", "Film");
    }
}