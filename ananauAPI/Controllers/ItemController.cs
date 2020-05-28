using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ananauAPI.DTO;
using ananauAPI.Models;
using ananauAPI.Models.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace ananauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[AllowAnonymous]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet("byId/{itemId}")]
        public ActionResult<Item> GetItemById(string itemId)
        {
            Item i = _itemRepository.GetBy(itemId);
            if (i == null) return NotFound("itemMetIdNietGevonden");
            return i;
        }

        [HttpGet("byName/{naam}")]
        public ActionResult<Item> GetItemByName(string naam)
        {
            Item i = _itemRepository.GetByName(naam);
            if (i == null) return BadRequest("itemMetIdNietGevonden");
            return i;
        }

        [HttpGet("byContainsName/{itemNaam}")]
        public ActionResult<List<Item>> GetItemByContainsName(string itemNaam)
        {
            List<Item> items = _itemRepository.GetByContainsName(itemNaam).ToList();
            if(items.Count == 0) return NotFound("Er werd geen enkel item gevonden die dit woord bevat.");
            return items;
        }

        [HttpDelete("{id}")]
        public ActionResult<Item> VerwijderItem(string id)
        {
            Item g = _itemRepository.GetBy(id);
            if (g == null)
            {
                return NotFound("itemMetIdNietGevonden");
            }
            _itemRepository.Delete(g);
            _itemRepository.SaveChanges();
            return g;
        }

        [HttpGet("getAll")]
        public IEnumerable<Item> GetItems()
        {
            return _itemRepository.GetAll();
        }


        [HttpPost("getAllWithFilter")]
        public ActionResult<FilterItemsExportDTO> GetItemsWithFilter(filterItemsDTO filter)
        {
            var items = _itemRepository.GetAll().OrderBy(t => t.Naam).ToList();
            if (filter.NaamSorterenASC && filter.NaamSorterenDESC &&
                filter.BeschikbaarSorterenASC && filter.BeschikbaarSorterenDESC &&
                filter.ToegevoegdOpSorterenASC && filter.ToegevoegdOpSorterenDESC
                )
            {
                return BadRequest("eenItemTergelijk");
            }

            if (filter.NaamSorterenDESC)
            {
                items = items.OrderByDescending(t => t.Naam).ToList();
            }
            else if (filter.NaamSorterenASC)
            {
                items = items.OrderBy(t => t.Naam).ToList();
            }
            else if (filter.BeschikbaarSorterenDESC)
            {
                items = items.OrderByDescending(t => t.Beschikbaar).ToList();
            }
            else if (filter.BeschikbaarSorterenASC)
            {
                items = items.OrderBy(t => t.Beschikbaar).ToList();
            }
            else if (filter.ToegevoegdOpSorterenDESC)
            {
                items = items.OrderByDescending(t => t.ToegevoegdOp).ToList();
            }
            else if (filter.ToegevoegdOpSorterenASC)
            {
                items = items.OrderBy(t => t.ToegevoegdOp).ToList();
            }

            if (filter.ItemFilter != "")
            {
                items = items.Where(t => t.Naam.ToLower().Contains(filter.ItemFilter.ToLower())).ToList();
            }

            if (filter.BeschikbaarFilter == "ja")
            {
                items = items.Where(t => t.Beschikbaar == true).ToList();
            }
            else if (filter.BeschikbaarFilter == "nee")
            {
                items = items.Where(t => t.Beschikbaar == false).ToList();
            }

            if (filter.Gearchiveerd == true)
            {
                items = items.Where(t => t.Gearchiveerd == true).ToList();
            }
            else if(filter.Gearchiveerd == false)
            {
                items = items.Where(t => t.Gearchiveerd == false).ToList();
            }

            if (filter.CategorieFilter != "-1")
            {
                items = items.Where(t => t.Categorie.GetHashCode().ToString() == filter.CategorieFilter).ToList();
            }
            

            int aantalItems = items.Count;
            items = items.Skip(filter.itemsVanaf).Take(filter.aantalItems).ToList();

            return new FilterItemsExportDTO(items, aantalItems);
        }

        [HttpPut("{id}")]
        public ActionResult<Item> PutItem(string id, ItemDTO item)
        {
            if (!item.Id.Equals(id))
            {
               return BadRequest("idsKomenNietOvereen");
            }

            Item i = _itemRepository.GetBy(id);
            if(i.Naam != item.Naam)
            {
                if(_itemRepository.GetByName(item.Naam) != null)
                {
                    return BadRequest("kiesAndereNaam");
                }

            } 
            i.Naam = item.Naam;
            if (item.Gearchiveerd)
            {
                if (i.Beschikbaar)
                {
                    i.Gearchiveerd = item.Gearchiveerd;
                }
                else
                {
                    return BadRequest("uitegeleendItemsNietArchiveren");
                }
            }
            else
            {
                i.Gearchiveerd = item.Gearchiveerd;
            }
     
            _itemRepository.Update(i);
            _itemRepository.SaveChanges();
            return i;
        }

        [HttpPost]
        public ActionResult<Item> VoegItemToe(ItemDTO itemDto)
        {
            if (_itemRepository.GetByName(itemDto.Naam) != null)
            {
                return BadRequest("kiesAndereNaam");
            }
            Item item = new Item(itemDto.Naam);
            item.Materiaal = itemDto.Materiaal;
            item.Merk = itemDto.Merk;
            item.AankoopDatum = itemDto.AankoopDatum;
                item.Inhoud = itemDto.Inhoud;
            item.Categorie = itemDto.Categorie;
            _itemRepository.Add(item);
            _itemRepository.SaveChanges();
            return item;
        }

        
    }
}
