﻿using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations;

public class SlideEntityConfiguration : IEntityTypeConfiguration<SlideEntity>
{
    public void Configure(EntityTypeBuilder<SlideEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.HasIndex(r => r.Name).IsUnique();
        builder.ToTable("Slides", "Film");
    }
}