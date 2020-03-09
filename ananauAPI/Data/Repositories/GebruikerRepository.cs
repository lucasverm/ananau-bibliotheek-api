using System;
using System.Collections.Generic;
using System.Linq;
using ananauAPI.Models;
using ananauAPI.Models.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ananauAPI.Data.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Gebruiker> _gebruikers;

        public GebruikerRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _gebruikers = dbContext.Gebruikers;
        }

        public void Add(Gebruiker gebruiker)
        {
            _gebruikers.Add(gebruiker);
        }

        public void Delete(Gebruiker gebruiker)
        {
            _gebruikers.Remove(gebruiker);
        }

        public IEnumerable<Gebruiker> GetAll()
        {
            return _gebruikers.ToList();
        }

        public Gebruiker GetBy(string id)
        {
            return _gebruikers.Include(r => r.Items).SingleOrDefault(r => r.Id == id);
        }

        public bool TryGetGebruiker(string name, out Gebruiker gebruiker)
        {
            gebruiker = _gebruikers.FirstOrDefault(u => u.Email == name);
            return gebruiker != null;
        }

        public Gebruiker GetByEmail(string email)
        {
            return _gebruikers.Include(r => r.Items).SingleOrDefault(r => r.Email == email);
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
