using System;
using ananauAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ananauAPI.Data.Mappers
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {

            builder.Property(g => g.Naam).IsRequired();
            builder.HasMany(g => g.Gebruikers).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
