using System;
using System.Collections.Generic;

namespace ananauAPI.Models.RepositoryInterfaces
{
    public interface IApplicatieRepository
    {
        Applicatie GetBy(string id);
        Applicatie GetByEmailAchternaam(string email, string achternaam);
        Applicatie GetByEmail(string email);
        List<Applicatie> GetAll();
        Applicatie Add(Applicatie applicatie);
        Applicatie Delete(Applicatie applicatie);
        Applicatie Update(Applicatie applicatie);
        void SaveChanges();
    }
}
