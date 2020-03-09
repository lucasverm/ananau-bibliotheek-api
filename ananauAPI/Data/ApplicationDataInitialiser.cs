﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ananauAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace ananauAPI.Data
{
    public class ApplicationDataInitialiser
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Gebruiker> _userManager;

        public ApplicationDataInitialiser(ApplicationDbContext dbContext, UserManager<Gebruiker> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {


            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            
                {
                Gebruiker lucas = new Gebruiker("lucas", "vermeulen","lucasvermeulen@gmail.com","testFoto");
                var gebruikers = new List<Gebruiker> { lucas };
                foreach (Gebruiker g in gebruikers)
                {
                    await MaakGebruiker(g, "Test123!");
                }
                _dbContext.SaveChanges();

            }

        }

        private async Task MaakGebruiker(Gebruiker gebruiker, string password)
        {
            await _userManager.CreateAsync(gebruiker, password);
        }
    }
}