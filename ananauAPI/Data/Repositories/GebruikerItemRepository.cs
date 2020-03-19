using System;
using System.Collections.Generic;
using System.Linq;
using ananauAPI.DTO;
using ananauAPI.Models;
using ananauAPI.Models.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ananauAPI.Data.Repositories
{
    public class GebruikerItemRepository : IGebruikerItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<GebruikerItem> _gebruikerItems;

        public GebruikerItemRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _gebruikerItems = dbContext.GebruikerItems;
        }

        public void Add(GebruikerItem gebruikerItem)
        {
            _gebruikerItems.Add(gebruikerItem);
        }

        public void Delete(GebruikerItem gebruikerItem)
        {
            _gebruikerItems.Remove(gebruikerItem);
        }

        public IEnumerable<GebruikerItem> GetAll()
        {
            return _gebruikerItems.Include(t => t.Gebruiker).Include(t => t.Item).ToList();    
        }

        public GebruikerItem GetBy(string id)
        {
            return _gebruikerItems.Include(t => t.Gebruiker).Include(t => t.Item).SingleOrDefault(r => r.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(GebruikerItem gebruikerItem)
        {
            _context.Update(gebruikerItem);
        }

        public GebruikerItem vindOpenStaandeLeningMetItemId(string id)
        {
            return _gebruikerItems.Include(t => t.Gebruiker).Include(t => t.Item).SingleOrDefault(r => r.Item.Id == id && r.TerugOp == null);
        }
    }
}
