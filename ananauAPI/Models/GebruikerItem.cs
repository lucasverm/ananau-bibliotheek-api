using System;
using Newtonsoft.Json;

namespace ananauAPI.Models
{
    public class GebruikerItem
    {
        public string Id { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public Item Item { get; set; }
        public DateTime OntleendOp{ get; set; }
        public DateTime TerugOp { get; set; }

        public GebruikerItem()
        {
            OntleendOp = DateTime.Now;
        }
        public GebruikerItem(Gebruiker g, Item i):this()
        {
            Gebruiker = g;
            Item = i;

        }

    }
}
