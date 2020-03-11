using System;
using System.Collections.Generic;
using System.Linq;
using ananauAPI.DTO;
using ananauAPI.Models;
using ananauAPI.Models.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ananauAPI.Data.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Gebruiker> _gebruikers;
        private readonly DbSet<GebruikerItem> _gebruikersItems;

        public GebruikerRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _gebruikers = dbContext.Gebruikers;
            _gebruikersItems = dbContext.GebruikerItems;
        }

        public void Add(Gebruiker gebruiker)
        {
            _gebruikers.Add(gebruiker);
        }

        public Gebruiker GetByEmail(string email)
        {
            return _gebruikers.Include(t => t.GebruikerItems).ThenInclude(a => a.Gebruiker).Include(a => a.GebruikerItems).ThenInclude(a => a.Item).SingleOrDefault(r => r.Email == email);
        }


        public void Delete(Gebruiker gebruiker)
        {
            _gebruikers.Remove(gebruiker);
        }

        public IEnumerable<Gebruiker> GetAll()
        {
            return _gebruikers.Include(t => t.GebruikerItems).ThenInclude(a => a.Gebruiker).Include(a => a.GebruikerItems).ThenInclude(a => a.Item).ToList();
             
        }

        public Gebruiker GetBy(string id)
        {
            return _gebruikers.Include(t => t.GebruikerItems).ThenInclude(a => a.Gebruiker).Include(a => a.GebruikerItems).ThenInclude(a => a.Item).SingleOrDefault(r => r.Id == id);
        }

        public bool TryGetGebruiker(string name, out Gebruiker gebruiker)
        {
            gebruiker = _gebruikers.FirstOrDefault(u => u.Email == name);
            return gebruiker != null;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Gebruiker gebruiker)
        {
            _context.Update(gebruiker);
        }
    }
}
