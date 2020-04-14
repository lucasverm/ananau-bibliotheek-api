using System;
using System.Collections.Generic;

namespace ananauAPI.Models
{
    public class Item
    {
        public string Naam { get; set; }
        public string Id { get; set; }
        public ItemCategorie Categorie { get; set; }
        public string Materiaal { get; set; }
        public string Merk { get; set; }
        public string Inhoud { get; set; }
        public List<GebruikerItem> GebruikerItems { get; set; }
        public Boolean Beschikbaar { get; set; }
        public Boolean Gearchiveerd { get; set; }
        public DateTime ToegevoegdOp { get; set; }
        public DateTime? AankoopDatum { get; set; }
        public Item()
        {
            GebruikerItems = new List<GebruikerItem>();
            Beschikbaar = true;
            Gearchiveerd = false;
            ToegevoegdOp = DateTime.Now;
        }

        public Item(string naam) : this()
        {
            Naam = naam;
        }
    }
}