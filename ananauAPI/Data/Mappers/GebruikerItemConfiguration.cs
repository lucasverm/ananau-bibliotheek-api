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

            builder.HasKey(gi => (gi.Gebruiker.Id + gi.Item.Id));

            builder.HasOne(gi => gi.Item)
                .WithMany(da => da.Gebruikers)
                .HasForeignKey(gi => gi.Item.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(gi => gi.Gebruiker)
                .WithMany()
                .HasForeignKey(gi => gi.Id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
