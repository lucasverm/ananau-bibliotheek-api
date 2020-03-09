﻿using System;
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

        public ItemRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _Items = dbContext.Items;
        }

        public void Add(Item Item)
        {
            _Items.Add(Item);
        }

        public void Delete(Item Item)
        {
            _Items.Remove(Item);
        }

        public IEnumerable<Item> GetAll()
        {
            return _Items.ToList();
        }

        public Item GetBy(string id)
        {
            return _Items.Include(r => r.Gebruikers).SingleOrDefault(r => r.Id == id);
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