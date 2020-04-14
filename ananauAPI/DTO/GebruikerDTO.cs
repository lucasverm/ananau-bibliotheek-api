using System;
using System.Collections.Generic;
using System.Linq;
using ananauAPI.Models;

namespace ananauAPI.DTO
{
    public class GebruikerDTO
    {
        public string Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public List<GebruikerItem> GebruikerItems { get; set; }
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }

        public GebruikerDTO()
        {
        }

        public GebruikerDTO(Gebruiker g)
        {
            Id = g.Id;
            Voornaam = g.Voornaam;
            Achternaam = g.Achternaam;
            GebruikerItems = g.GebruikerItems;
            Email = g.Email;
            TelefoonNummer = g.TelefoonNummer;
            GeboorteDatum = g.GeboorteDatum;
        }

        
    }
}
