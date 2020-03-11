using System;
using Newtonsoft.Json;

namespace ananauAPI.Models
{
    public class GebruikerItem
    {
        public string Id { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public Item Item { get; set; }

        public GebruikerItem(){}

        public GebruikerItem(Gebruiker g, Item i)
        {
            Gebruiker = g;
            Item = i;
        }

    }
}
