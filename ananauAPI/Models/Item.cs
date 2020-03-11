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
        public List<GebruikerItem> GebruikerItems { get; set; }
        public Boolean Beschikbaar { get; set; }
        public Item()
        {
            GebruikerItems = new List<GebruikerItem>();
            Beschikbaar = true;
        }

        public Item(string naam) : this()
        {
            Naam = naam;
        }
    }
}