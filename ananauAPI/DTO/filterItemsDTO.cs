using System;
namespace ananauAPI.DTO
{
    public class filterItemsDTO
    {
        public int itemsVanaf { get; set; }
        public int aantalItems { get; set; }
        public Boolean NaamSorterenASC { get; set; }
        public Boolean NaamSorterenDESC{ get; set; }
        public Boolean BeschikbaarSorterenASC { get; set; }
        public Boolean BeschikbaarSorterenDESC { get; set; }
        public Boolean ToegevoegdOpSorterenASC { get; set; }
        public Boolean ToegevoegdOpSorterenDESC { get; set; }
        public String ItemFilter { get; set; }
        public String BeschikbaarFilter { get; set; }
        public Boolean Gearchiveerd { get; set; }
        public String CategorieFilter { get; set; }
        public filterItemsDTO(){}
    }
}
