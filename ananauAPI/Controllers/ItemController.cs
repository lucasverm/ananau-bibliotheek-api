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

        [HttpGet("{itemId}")]
        public ActionResult<ItemExportDTO> GetItemById(string itemId)
        {
            Item i = _itemRepository.GetBy(itemId);
            if (i == null) return NotFound();
            return new ItemExportDTO(i);
        }

        [HttpDelete("{id}")]
        public ActionResult<Item> VerwijderItem(string id)
        {
            Item g = _itemRepository.GetBy(id);
            if (g == null)
            {
                return NotFound();
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
            Item item = new Item(itemDto.Naam);

            _itemRepository.Add(item);
            _itemRepository.SaveChanges();
            return new ItemExportDTO(item);
        }

        
    }
}
