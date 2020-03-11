using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ananauAPI.DTO;
using ananauAPI.Models;
using ananauAPI.Models.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ananauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OntleenController : Controller
    {
        private readonly UserManager<Gebruiker> _userManager;
        private readonly IItemRepository _itemRepository;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IGebruikerItemRepository _gebruikerItemRepository;
        public OntleenController(IItemRepository itemRepository, UserManager<Gebruiker> userManager, IGebruikerRepository gebruikerRepository, IGebruikerItemRepository gebruikerItemRepository)
        {
            _itemRepository = itemRepository;
            _gebruikerRepository = gebruikerRepository;
            _gebruikerItemRepository = gebruikerItemRepository;
            _userManager = userManager;
        }

        [HttpPost]
        public ActionResult<GebruikerItemDTO> ScanItem(string itemId)
        {
            GebruikerItem gi;
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(HttpContext.User.Identity.Name);
            Console.WriteLine("------------------------------------------------------");
            Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail(User.Identity.Name);

            //Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail("user@example.com");
            if (huidigeGebruiker == null) return NotFound("Gebruiker niet gevonden!");
            var item = _itemRepository.GetBy(itemId);
            if (item == null) return NotFound("Item niet gevonden!");
            if (item.Beschikbaar)
            {
                gi = new GebruikerItem(huidigeGebruiker, item);
                _gebruikerItemRepository.Add(gi);
                huidigeGebruiker.GebruikerItems.Add(gi);
                _gebruikerRepository.Update(huidigeGebruiker);
                item.GebruikerItems.Add(gi);
            }
            else
            {
                gi = _gebruikerItemRepository.vindOpenStaandeLeningMetItemId(itemId);
                if (gi == null)
                    return NotFound();
                gi.TerugOp = DateTime.Now;
                _gebruikerItemRepository.Update(gi);
            }
            item.Beschikbaar = !item.Beschikbaar;
            _itemRepository.Update(item);
            _itemRepository.SaveChanges();
            return new GebruikerItemDTO(gi);
        }
    }
}
