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

        public ItemDTO()
        {
        }

        public ItemDTO(Item i)
        {
            Id = i.Id;
            Naam = i.Naam;

        }
    }
}
