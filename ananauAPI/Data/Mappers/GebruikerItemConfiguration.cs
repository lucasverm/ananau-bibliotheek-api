using System;
using ananauAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ananauAPI.Data.Mappers
{
    public class GebruikerItemConfiguration : IEntityTypeConfiguration<GebruikerItem>
    {
        public void Configure(EntityTypeBuilder<GebruikerItem> builder)
        {
            builder.HasOne(g => g.Gebruiker).WithMany().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(g => g.Item).WithMany().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
