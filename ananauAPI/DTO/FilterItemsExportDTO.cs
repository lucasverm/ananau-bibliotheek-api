using System;
using System.Collections.Generic;
using ananauAPI.Models;

namespace ananauAPI.DTO
{
    public class FilterItemsExportDTO
    {
        public List<Item> Items;
        public int Totaal;
        public FilterItemsExportDTO(){}
        public FilterItemsExportDTO(List<Item> items, int totaal)
        {
            this.Items = items;
            this.Totaal = totaal;
        }
    }
}
