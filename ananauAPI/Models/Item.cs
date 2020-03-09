using System;
using System.Collections.Generic;

namespace ananauAPI.Models
{
    public class Item
    {

        private string _naam;
        public string Id { get; set; }
        public string Naam
        {
            get
            {
                return _naam;
            }
            set
            {            
                _naam = value;
            }
        }
        public List<GebruikerItem> Gebruikers { get; set; }

        public Item()
        {
        }

        public Item(string naam) : this()
        {
            Naam = naam;
        }
    }
}