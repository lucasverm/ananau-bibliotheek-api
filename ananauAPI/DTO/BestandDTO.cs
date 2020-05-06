using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

namespace ananauAPI.DTO
{
    public class BestandDTO
    {
        public string applicatieId { get; set; }
        public string folder { get; set; }
        public string bestandNaam { get; set; }
        [FromForm(Name = "bestand")]
        public FormFile bestand { get; set; }
    }
}
