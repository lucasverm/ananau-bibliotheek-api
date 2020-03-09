using System;
using System.Collections.Generic;

namespace ananauAPI.Models.RepositoryInterfaces
{
    public interface IGebruikerRepository
    {
        Gebruiker GetBy(string id);
        Gebruiker GetByEmail(string email);
        bool TryGetGebruiker(string name, out Gebruiker gebruiker);
        IEnumerable<Gebruiker> GetAll();
        void Add(Gebruiker gebruiker);
        void Delete(Gebruiker gebruiker);
        void Update(Gebruiker gebruiker);
        void SaveChanges();
    }
}
