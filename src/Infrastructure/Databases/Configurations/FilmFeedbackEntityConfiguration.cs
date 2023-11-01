using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations;

public class FilmFeedbackEntityConfiguration : IEntityTypeConfiguration<FilmFeedbackEntity>
{
    public void Configure(EntityTypeBuilder<FilmFeedbackEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.ToTable("FilmFeedback", "Film");
    }
}