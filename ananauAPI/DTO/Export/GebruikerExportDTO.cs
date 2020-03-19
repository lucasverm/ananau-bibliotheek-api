using System;
using System.Collections.Generic;
using System.Linq;
using ananauAPI.Models;

namespace ananauAPI.DTO
{
    public class GebruikerExportDTO
    {
        public string Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public List<GebruikerItemDTO> GebruikerItems { get; set; }
        public string Email { get; set; }

        public GebruikerExportDTO()
        {
        }

        public GebruikerExportDTO(Gebruiker g)
        {
            Id = g.Id;
            Voornaam = g.Voornaam;
            Achternaam = g.Achternaam;
            GebruikerItems = g.GebruikerItems.Select(t => new GebruikerItemDTO(t)).ToList();
            Email = g.Email;
        }

    }
}
