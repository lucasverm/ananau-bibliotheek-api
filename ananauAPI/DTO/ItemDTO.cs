using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ananauAPI.Models;

namespace ananauAPI.DTO
{
    public class ItemDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Naam is verplicht!")]
        public string Naam { get; set; }
        public Boolean Gearchiveerd { get; set; }
        public string Materiaal { get; set; }
        public string Merk { get; set; }
        public DateTime? AankoopDatum { get; set; }
        public string Inhoud { get; set; }
        public ItemCategorie Categorie { get; set; }
        public ItemDTO()
        {
        }

        public ItemDTO(Item i)
        {
            Id = i.Id;
            Naam = i.Naam;
            Merk = i.Merk;
            Inhoud = i.Inhoud;
            Materiaal = i.Materiaal;
            Categorie = i.Categorie;
            AankoopDatum = i.AankoopDatum;

        }
    }
}
