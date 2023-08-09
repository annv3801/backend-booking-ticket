using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations;
public class FilmSchedulesConfiguration : IEntityTypeConfiguration<FilmSchedule>
{
    public void Configure(EntityTypeBuilder<FilmSchedule> builder)
    {
        builder.HasKey(r => r.Id);
        builder.ToTable("FilmSchedules");
    }
}
