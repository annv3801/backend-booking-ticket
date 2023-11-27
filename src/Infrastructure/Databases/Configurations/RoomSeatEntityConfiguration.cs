using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations;

public class RoomSeatEntityConfiguration : IEntityTypeConfiguration<RoomSeatEntity>
{
    public void Configure(EntityTypeBuilder<RoomSeatEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.HasIndex(r => r.Name).IsUnique();
        builder.ToTable("RoomSeats", "Film");
    }
}