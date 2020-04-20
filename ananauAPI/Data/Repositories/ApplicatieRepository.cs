using System;
using System.Collections.Generic;
using System.Linq;
using ananauAPI.Models;
using ananauAPI.Models.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ananauAPI.Data.Repositories
{
    public class ApplicatieRepository : IApplicatieRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Applicatie> _applicaties;

        public ApplicatieRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _applicaties = dbContext.Applicaties;
        }

        public Applicatie Add(Applicatie applicatie)
        {
            _applicaties.Add(applicatie);
            return applicatie;
        }

        public Applicatie Delete(Applicatie applicatie)
        {
            _applicaties.Remove(applicatie);
            return applicatie;
        }

        public List<Applicatie> GetAll()
        {
            return _applicaties.ToList();
        }

        public Applicatie GetBy(string id)
        {
            return _applicaties.FirstOrDefault(t => t.Id == id);
        }

        public Applicatie GetByEmailAchternaam(string email, string achternaam)
        {
            return _applicaties.FirstOrDefault(t => t.Email == email && t.Achternaam == achternaam);
        }

        public Applicatie GetByEmail(string email)
        {
            return _applicaties.FirstOrDefault(t => t.Email == email);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Applicatie Update(Applicatie applicatie)
        {
            _applicaties.Update(applicatie);
            return applicatie;
        }
    }
}
