using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ananauAPI.Models;

namespace ananauAPI.DTO
{
    public class ItemExportDTO
    {
        public string Id { get; set; }
        public string Naam { get; set; }
        public List<GebruikerItemDTO> GebruikerItems { get; set; }
        public Boolean Beschikbaar { get; set; }

        public ItemExportDTO(){}
        public ItemExportDTO(Item i)
        {
            Id = i.Id;
            Naam = i.Naam;
            GebruikerItems = i.GebruikerItems.Select(t => new GebruikerItemDTO(t)).ToList();
            Beschikbaar = i.Beschikbaar;
        }
    }
}
