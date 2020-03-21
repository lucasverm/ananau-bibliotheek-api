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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet("byId/{itemId}")]
        public ActionResult<ItemExportDTO> GetItemById(string itemId)
        {
            Item i = _itemRepository.GetBy(itemId);
            if (i == null) return NotFound("Het item met opgegeven id kon niet worden gevonden.");
            return new ItemExportDTO(i);
        }

        [HttpGet("byName/{itemNaam}")]
        public ActionResult<List<ItemExportDTO>> GetItemByName(string itemNaam)
        {
            List<Item> items = _itemRepository.GetByName(itemNaam).ToList();
            List<ItemExportDTO> uitvoer = items.Select(i => new ItemExportDTO(i)).ToList();
            if(uitvoer.Count == 0) return NotFound("Er werd geen enkel item gevonden die dit woord bevat.");
            return uitvoer;
        }

        [HttpDelete("{id}")]
        public ActionResult<Item> VerwijderItem(string id)
        {
            Item g = _itemRepository.GetBy(id);
            if (g == null)
            {
                return NotFound("Het item met opgegeven id kon niet worden gevonden.");
            }
            _itemRepository.Delete(g);
            _itemRepository.SaveChanges();
            return g;
        }

        [HttpGet]
        public IEnumerable<ItemExportDTO> GetItems()
        {
            return _itemRepository.GetAll().Select(g => new ItemExportDTO(g));
        }

        [HttpPut("{id}")]
        public ActionResult<Item> PutGebruiker(string id, ItemDTO item)
        {
            if (!item.Id.Equals(id))
                return BadRequest();

            Item i = _itemRepository.GetBy(id);

            i.Naam = item.Naam;
            _itemRepository.Update(i);
            _itemRepository.SaveChanges();
            return NoContent();
        }

        [HttpPost]
        public ActionResult<ItemExportDTO> VoegItemToe(ItemDTO itemDto)
        {

            if (_itemRepository.GetByName(itemDto.Naam) != null) return BadRequest("Er betaat al een item met deze naam! Geef een andere naam op!");
            Item item = new Item(itemDto.Naam);

            _itemRepository.Add(item);
            _itemRepository.SaveChanges();
            return new ItemExportDTO(item);
        }

        
    }
}
