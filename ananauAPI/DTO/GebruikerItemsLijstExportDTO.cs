using System;
using System.Collections.Generic;
using ananauAPI.Models;

namespace ananauAPI.DTO
{
    public class GebruikerItemsLijstExportDTO
    {
        public List<GebruikerItem> GebruikerItems;
        public int Totaal;

        public GebruikerItemsLijstExportDTO(){}
        public GebruikerItemsLijstExportDTO(List<GebruikerItem> items, int totaal)
        {
            this.GebruikerItems = items;
            this.Totaal = totaal;
        }
    }
}
