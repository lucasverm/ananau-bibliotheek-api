using System;
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
                //Gebruiker lucas = new Gebruiker("lucas", "vermeulen", "lucasvermeulen@gmail.com", "stringFoto");

                Gebruiker stringUser = new Gebruiker("string", "string", "user@example.com", "testFoto");
                var gebruikers = new List<Gebruiker> { stringUser };
                foreach (Gebruiker g in gebruikers)
                {
                    await MaakGebruiker(g, "string");
                }

                Item it1 = new Item("Boek");
                _dbContext.Items.Add(it1);
                _dbContext.SaveChanges();

               // GebruikerItem gi = new GebruikerItem(lucas, it1);

                //_dbContext.GebruikerItems.Add(gi);
                //lucas.GebruikerItems.Add(gi);
                //it1.GebruikerItems.Add(gi);

                _dbContext.SaveChanges();
            }

        }

        private async Task MaakGebruiker(Gebruiker gebruiker, string password)
        {
            await _userManager.CreateAsync(gebruiker, password);
        }
    }
}
