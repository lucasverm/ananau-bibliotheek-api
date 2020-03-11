using System;
using System.Collections.Generic;
using ananauAPI.DTO;

namespace ananauAPI.Models.RepositoryInterfaces
{
    public interface IGebruikerItemRepository
    {
        GebruikerItem GetBy(string id);
        IEnumerable<GebruikerItem> GetAll();
        void Add(GebruikerItem gebruikerItem);
        void Delete(GebruikerItem gebruikerItem);
        void Update(GebruikerItem gebruikerItem);
        void SaveChanges();
        GebruikerItem vindOpenStaandeLeningMetItemId(string id);
    }
}
