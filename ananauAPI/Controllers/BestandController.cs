using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.IdentityModel.Xml;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using ananauAPI.Models.RepositoryInterfaces;
using ananauAPI.Models;
using ananauAPI.DTO;
using System.Collections.Generic;

namespace ananauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [AllowAnonymous]
    public class BestandController : ControllerBase
    {
        private readonly string[] aanvaardeBestandExtenties;
        private readonly IApplicatieRepository _applicatieRepository;

        public BestandController(IApplicatieRepository applicatieRepository)
        {
            aanvaardeBestandExtenties = new string[] { "image/png", "image/jpeg", "image/jpg" };
            _applicatieRepository = applicatieRepository;

        }

        [HttpPost]
        public Task<ActionResult> UploadBestand(List<BestandDTO> bestanden)
        {
            string req = null;
            bestanden.ForEach(async bestand =>
           {
               Applicatie applicatie = _applicatieRepository.GetBy(bestand.applicatieId);
               if (applicatie == null) req = "De applicatie bestaat niet!";
               if (bestand.bestandNaam.Contains("reispaspoort"))
               {
                   applicatie.ReispaspoortNaam = bestand.bestandNaam;
               }
               else if (bestand.bestandNaam.Contains("attest"))
               {
                   applicatie.attestNaam = bestand.bestandNaam;
               }
               else if (bestand.bestandNaam.Contains("diploma"))
               {
                   applicatie.diplomaNaam = bestand.bestandNaam;
               }
               else
               {
                   BadRequest("De bestandsnaam is niet correct!");
               }
               _applicatieRepository.Update(applicatie);
               _applicatieRepository.SaveChanges();
               if (!aanvaardeBestandExtenties.Contains(bestand.bestand.ContentType))
               {
                   req = "Extentie niet correct!";
               }
               var folderPad = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), bestand.folder);
               if (!Directory.Exists(folderPad))
               {
                   Directory.CreateDirectory(folderPad);
               }
               var bestandPad = Path.Combine(folderPad, bestand.bestandNaam);
               if (System.IO.File.Exists(bestandPad))
               {
                   System.IO.File.Delete(bestandPad);
               }
               using (var fileStream = new FileStream(bestandPad, FileMode.Create))
               {
                   await bestand.bestand.CopyToAsync(fileStream);
               }
           });
            if (req != null)
            {
                //return BadRequest(req);
            }
            else
            {
                //return NoContent();
            }
            return null;
        }

        [HttpGet("{applicatieId}/{folder}/{bestandNaam}")]
        public IActionResult Get(string applicatieId, string folder, string bestandNaam)
        {
            Applicatie applicatie = _applicatieRepository.GetBy(applicatieId);
            if (applicatie == null) return BadRequest("De applicatie bestaat niet!");
            if (bestandNaam.Contains("reispaspoort"))
            {
                bestandNaam = applicatie.ReispaspoortNaam;
            }
            else if (bestandNaam.Contains("attest"))
            {
                bestandNaam = applicatie.attestNaam;
            }
            else if (bestandNaam.Contains("diploma"))
            {
                bestandNaam = applicatie.diplomaNaam;
            }
            else
            {
                BadRequest("De bestandsnaam is niet correct!");
            }
            Byte[] b;
            var folderPad = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), folder);
            if (!Directory.Exists(folderPad))
            {
                return NoContent();
            }
            var bestandPad = Path.Combine(folderPad, bestandNaam);
            if (!System.IO.File.Exists(bestandPad))
            {
                return NoContent();
            }
            new FileExtensionContentTypeProvider().TryGetContentType(bestandNaam, out string contentType);
            b = System.IO.File.ReadAllBytes(bestandPad);
            return File(b, contentType);
        }

    }
}