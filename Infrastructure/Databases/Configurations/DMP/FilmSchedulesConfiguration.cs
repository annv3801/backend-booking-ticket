using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations.DMP;
public class FilmSchedulesConfiguration : IEntityTypeConfiguration<FilmSchedule>
{
    public void Configure(EntityTypeBuilder<FilmSchedule> builder)
    {
        builder.HasKey(r => r.Id);
        builder.ToTable("DMP_FilmSchedules");
    }
}
