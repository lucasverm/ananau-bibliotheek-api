using System;
using System.Collections.Generic;
using ananauAPI.Models;

namespace ananauAPI.DTO
{
    public class GebruikerDTO
    {
        public string Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        //public List<GebruikerItem> Items { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }

        public GebruikerDTO()
        {
        }

            public GebruikerDTO(Gebruiker g)
        {
            Id = g.Id;
            Voornaam = g.Voornaam;
            Achternaam = g.Achternaam;
            //Items = g.Items;
            Email = g.Email;
            Foto = g.Foto;
        }
    }
}
