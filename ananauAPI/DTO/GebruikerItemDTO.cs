using System;
using ananauAPI.Models;

namespace ananauAPI.DTO
{
    public class GebruikerItemDTO
    {
        public string Id { get; set; }
        public string ItemId { get; set; }
        public string ItemNaam { get; set; }
        public string GebruikerId { get; set; }
        public string GebruikerVoornaam { get; set; }
        public string GebruikerAchternaam { get; set; }
        public DateTime OntleendOp { get; set; }
        public DateTime?
            TerugOp { get; set; }

        public GebruikerItemDTO(){}
        public GebruikerItemDTO(GebruikerItem gi)
        {
            Id = gi.Id;
            ItemId = gi.Item.Id;
            ItemNaam = gi.Item.Naam;
            GebruikerId = gi.Gebruiker.Id;
            GebruikerVoornaam = gi.Gebruiker.Voornaam;
            GebruikerAchternaam = gi.Gebruiker.Achternaam;
            OntleendOp = gi.OntleendOp;
            TerugOp = gi.TerugOp;
        }
    }
}
