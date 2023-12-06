using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations;

public class BookingDetailEntityConfiguration : IEntityTypeConfiguration<BookingDetailEntity>
{
    public void Configure(EntityTypeBuilder<BookingDetailEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.ToTable("BookingDetails", "Film");
    }
}