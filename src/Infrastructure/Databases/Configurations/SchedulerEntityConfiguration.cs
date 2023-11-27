using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations;

public class SchedulerEntityConfiguration : IEntityTypeConfiguration<SchedulerEntity>
{
    public void Configure(EntityTypeBuilder<SchedulerEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.ToTable("Schedulers", "Film");
    }
}