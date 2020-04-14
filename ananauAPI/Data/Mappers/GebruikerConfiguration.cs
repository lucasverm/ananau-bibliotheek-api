using System;
using ananauAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ananauAPI.Data.Mappers
{
    public class GebruikerConfiguration : IEntityTypeConfiguration<Gebruiker>
    {
        public void Configure(EntityTypeBuilder<Gebruiker> builder)
        {
            builder.Property(g => g.Achternaam).IsRequired();
            builder.Property(g => g.Voornaam).IsRequired();
            builder.Property(g => g.Email).IsRequired();
            builder.Property(g => g.GeboorteDatum).IsRequired();
            builder.Property(g => g.TelefoonNummer).IsRequired();
        }
    }
}
