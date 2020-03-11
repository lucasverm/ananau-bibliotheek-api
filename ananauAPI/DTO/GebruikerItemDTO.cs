using System;
using ananauAPI.Models;

namespace ananauAPI.DTO
{
    public class GebruikerItemDTO
    {
        public string Id { get; set; }
        public string itemId { get; set; }
        public string itemNaam { get; set; }
        public string gebruikerId { get; set; }
        public string gebruikerVoornaam { get; set; }
        public GebruikerItemDTO()
        {

        }

        public GebruikerItemDTO(GebruikerItem gi)
        {
            Id = gi.Id;
            itemId = gi.Item.Id;
            itemNaam = gi.Item.Naam;
            gebruikerId = gi.Gebruiker.Id;
            gebruikerVoornaam = gi.Gebruiker.Voornaam;
        }
    }
}
