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
            aanvaardeBestandExtenties = new string[] { "image/png", "image/jpeg", "image/jpg", "application/pdf" };
            _applicatieRepository = applicatieRepository;

        }

        [HttpPost("{folder}/{bestandNaam}")]
        public async Task<ActionResult> UploadBestand(string folder, string bestandNaam, [FromForm(Name = "bestand")]IFormFile bestand)
        {
            if (!aanvaardeBestandExtenties.Contains(bestand.ContentType))
            {
                return BadRequest("Er is een extentie die niet aanvaard wordt!");
            }
            var folderPad = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), folder);
            string naam = "";
            DirectoryInfo d = new DirectoryInfo(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), folder));
            FileInfo[] Files = null;
            if (Directory.Exists(folderPad))
            {
                if (bestandNaam.Contains("reispaspoort"))
                {
                    Files = d.GetFiles("reispaspoort*"); //Getting Text files
                }
                else if (bestandNaam.Contains("attest"))
                {
                    Files = d.GetFiles("attest*");
                }
                else if (bestandNaam.Contains("diploma"))
                {
                    Files = d.GetFiles("diploma*");
                }
                
                foreach (FileInfo file in Files)
                {
                    naam = file.Name;
                }
            }
            
            var deletePath = Path.Combine(folderPad, naam);
            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }



            if (!Directory.Exists(folderPad))
            {
                Directory.CreateDirectory(folderPad);
            }
            var bestandPad = Path.Combine(folderPad, bestandNaam);
            using (var fileStream = new FileStream(bestandPad, FileMode.Create))
            {
                await bestand.CopyToAsync(fileStream);
            }
            return Ok();
        }

        [HttpGet("folderInfo/{folder}")]
        public ActionResult<List<String>> GetNamen(string applicatieId, string folder)
        {
            Applicatie applicatie = _applicatieRepository.GetBy(applicatieId);
            if (applicatie == null) return BadRequest("De applicatie bestaat niet!");

            DirectoryInfo d = new DirectoryInfo(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), folder));
            List<String> namen = d.GetFiles().Select(t => t.Name.ToString()).OrderBy(t => t).ToList();
            return namen;
        }

        
        [HttpGet("{applicatieId}/{bestandNaam}")]
        public IActionResult Get(string applicatieId, string bestandNaam)
        {
            bestandNaam = bestandNaam.ToLower();
            Applicatie applicatie = _applicatieRepository.GetBy(applicatieId);
            if (applicatie == null) return BadRequest("De applicatie bestaat niet!");
            var folderPad = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), applicatie.Email);
            DirectoryInfo d = new DirectoryInfo(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), applicatie.Email));
            string naam = "";
            FileInfo[] Files = null;
            if (Directory.Exists(folderPad))
            {
                if (bestandNaam.Contains("reispaspoort"))
                {
                    Files = d.GetFiles("reispaspoort*"); //Getting Text files
                }
                else if (bestandNaam.Contains("attest"))
                {
                    Files = d.GetFiles("attest*");
                }
                else if (bestandNaam.Contains("diploma"))
                {
                    Files = d.GetFiles("diploma*");
                }
                
                foreach (FileInfo file in Files)
                {
                    naam = file.Name;
                }
            }

            Byte[] b;
            
            if (!Directory.Exists(folderPad))
            {
                return NoContent();
            }
            var bestandPad = Path.Combine(folderPad, naam);
            if (!System.IO.File.Exists(bestandPad))
            {
                return NoContent();
            }
            new FileExtensionContentTypeProvider().TryGetContentType(naam, out string contentType);
            b = System.IO.File.ReadAllBytes(bestandPad);
            return File(b, contentType);
        }

    }
}