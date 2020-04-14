using System;
using System.Collections.Generic;
using System.Linq;
using ananauAPI.Models;
using ananauAPI.Models.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ananauAPI.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Item> _Items;
        private readonly DbSet<GebruikerItem> _GebruikerItems;

        public ItemRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _Items = dbContext.Items;
            _GebruikerItems = dbContext.GebruikerItems;
        }

        public void Add(Item Item)
        {
            _Items.Add(Item);
        }

        public void Delete(Item Item)
        {
            _GebruikerItems.RemoveRange(_GebruikerItems.Where(t => t.Item.Id == Item.Id).ToList());
            _Items.Remove(Item);
        }

        public IEnumerable<Item> GetAll()
        {
            return _Items.Include(r => r.GebruikerItems).ThenInclude(r => r.Gebruiker).Include(r => r.GebruikerItems).ThenInclude(r => r.Item).ToList();
        }

        public Item GetBy(string id)
        {
            return _Items.Include(r => r.GebruikerItems).ThenInclude(r => r.Gebruiker).Include(r => r.GebruikerItems).ThenInclude(r => r.Item).SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Item> GetByContainsName(string naam)
        {
            return _Items.Include(r => r.GebruikerItems).ThenInclude(r => r.Gebruiker).Include(r => r.GebruikerItems).ThenInclude(r => r.Item).Where(r => r.Naam.ToLower().Contains(naam.ToLower()) && r.Gearchiveerd == false);
        }

        public Item GetByName(string naam)
        {
            return _Items.Include(r => r.GebruikerItems).ThenInclude(r => r.Gebruiker).Include(r => r.GebruikerItems).ThenInclude(r => r.Item).SingleOrDefault(r => r.Naam.ToLower() == naam.ToLower());
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Item Item)
        {
            _context.Update(Item);
        }
    }
}
