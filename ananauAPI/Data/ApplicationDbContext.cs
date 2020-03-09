﻿using System;
using ananauAPI.Data.Mappers;
using ananauAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ananauAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new GebruikerConfiguration());

        }
    }
}
