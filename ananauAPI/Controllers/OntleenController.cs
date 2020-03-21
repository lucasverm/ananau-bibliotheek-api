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
    [AllowAnonymous]
    public class OntleenController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IGebruikerItemRepository _gebruikerItemRepository;
        public OntleenController(IItemRepository itemRepository, IGebruikerRepository gebruikerRepository, IGebruikerItemRepository gebruikerItemRepository)
        {
            _itemRepository = itemRepository;
            _gebruikerRepository = gebruikerRepository;
            _gebruikerItemRepository = gebruikerItemRepository;
        }

        [Authorize(Policy = "User")]
        [HttpPost("scan")]
        public ActionResult<GebruikerItem> ScanItem(string id)
        {
            GebruikerItem gi;
            Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail(User.Identity.Name);
            //Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail("user@example.com");
            if (huidigeGebruiker == null) return NotFound("Gebruiker niet gevonden!");
            var item = _itemRepository.GetBy(id);
            if (item == null) return NotFound("Item niet gevonden!");
            if (item.Beschikbaar)
            {
                gi = new GebruikerItem(huidigeGebruiker, item);
                gi.TerugOp = null;
                _gebruikerItemRepository.Add(gi);
                huidigeGebruiker.GebruikerItems.Add(gi);
                _gebruikerRepository.Update(huidigeGebruiker);
                item.GebruikerItems.Add(gi);
            }
            else
            {
                gi = _gebruikerItemRepository.vindOpenStaandeLeningMetItemId(id);
                if (gi == null)
                    return NotFound();
                gi.TerugOp = DateTime.Now;
                _gebruikerItemRepository.Update(gi);
            }
            item.Beschikbaar = !item.Beschikbaar;
            _itemRepository.Update(item);
            _itemRepository.SaveChanges();
            return gi;
        }

        [Authorize(Policy = "User")]
        [HttpGet("GetOntleendeBoekenVanGebruiker")]
        public ActionResult<GebruikerItemsLijstExportDTO> GetOntleendeBoekenVanGebruiker(int vanaf, int hoeveelheid)
        {
            Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail(User.Identity.Name);
            //Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail("user@example.com");
            if (huidigeGebruiker == null) return NotFound("Gebruiker niet gevonden!");
            List<GebruikerItem> items = huidigeGebruiker.GebruikerItems.Where(t => t.TerugOp == null).OrderBy(t => t.OntleendOp).Skip(vanaf - 1).Take(hoeveelheid).ToList();
            int totaal = huidigeGebruiker.GebruikerItems.Where(t => t.TerugOp == null).Count();
            return new GebruikerItemsLijstExportDTO(items, totaal);
        }

        [Authorize(Policy = "User")]
        [HttpGet("GetOntleenHistorieVanGebruiker")]
        public ActionResult<GebruikerItemsLijstExportDTO> GetOntleenHistorieVanGebruiker(int vanaf, int hoeveelheid)
        {
            Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail(User.Identity.Name);
            //Gebruiker huidigeGebruiker = _gebruikerRepository.GetByEmail("user@example.com");
            if (huidigeGebruiker == null) return NotFound("Gebruiker niet gevonden!");
            List<GebruikerItem> items = huidigeGebruiker.GebruikerItems.OrderByDescending(t => t.OntleendOp).Skip(vanaf - 1).Take(hoeveelheid).ToList();
            int totaal = huidigeGebruiker.GebruikerItems.Count();
            return new GebruikerItemsLijstExportDTO(items, totaal);
        }
    }
}
