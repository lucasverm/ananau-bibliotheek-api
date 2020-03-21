using System;
using System.Collections.Generic;

namespace ananauAPI.Models.RepositoryInterfaces
{
    public interface IItemRepository
    {
        Item GetBy(string id);
        IEnumerable<Item> GetByName(string naam);
        IEnumerable<Item> GetAll();
        void Add(Item item);
        void Delete(Item item);
        void Update(Item item);
        void SaveChanges();
    }
}
