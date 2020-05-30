using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            
                {/*
                //Gebruiker lucas = new Gebruiker("lucas", "vermeulen", "lucasvermeulen@gmail.com", "stringFoto");

                Gebruiker stringUser = new Gebruiker("string", "string", "user@example.com", new DateTime(1998,5,26), "+32495102770");
                var gebruikers = new List<Gebruiker> { stringUser };
                foreach (Gebruiker g in gebruikers)
                {
                    await MaakGebruiker(g, "string");
                }
                List<Item> items = new List<Item>();
                for (int i = 0; i < 100; i++)
                {
                    Item it1 = new Item(i.ToString());
                    if (i % 2 != 0)
                    {
                        it1.Beschikbaar = false;
                    }
                    items.Add(it1);
                }
                _dbContext.Items.AddRange(items);
                _dbContext.SaveChanges();

                List<GebruikerItem> gebruikerItems = new List<GebruikerItem>();
                foreach (var item in items.Select((value, i) => new { i, value }))
                {
                    GebruikerItem gi = new GebruikerItem(stringUser, item.value);
                    gi.OntleendOp.AddMonths(item.i);
                    if (item.i%2 == 0)
                    {
                        gi.TerugOp = DateTime.Now;
                    }
                    
                    gebruikerItems.Add(gi);
                    _dbContext.GebruikerItems.Add(gi);
                    stringUser.GebruikerItems.Add(gi);
                    item.value.GebruikerItems.Add(gi);
                }
               
                _dbContext.SaveChanges();*/
            }

        }

        private async Task MaakGebruiker(Gebruiker gebruiker, string password)
        {
            await _userManager.CreateAsync(gebruiker, password);
            await _userManager.AddClaimAsync(gebruiker, new Claim(ClaimTypes.Role, "User"));
            _dbContext.SaveChanges();
        }
    }
}
