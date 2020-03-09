using System;
using Newtonsoft.Json;

namespace ananauAPI.Models
{
    public class GebruikerItem
    {
        public string Id { get; set; }
        [JsonIgnore]
        public Item Item { get; set; }
        public Gebruiker Gebruiker { get; set; }

        protected GebruikerItem()
        {

        }

        public GebruikerItem(Gebruiker gebruiker, Item item)
        {
            Gebruiker = gebruiker;
            Item = item;
            Id = gebruiker.Id;
        }
    }
}
