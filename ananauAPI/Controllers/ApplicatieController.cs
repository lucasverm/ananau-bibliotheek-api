using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ananauAPI.DTO;
using ananauAPI.Models;
using ananauAPI.Models.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ananauAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [AllowAnonymous]
    public class ApplicatieController : ControllerBase
    {
        private readonly IApplicatieRepository _applicatieRepository;
        private SmtpClient smtpClient;
        public ApplicatieController(IApplicatieRepository applicatieRepository)
        {
            _applicatieRepository = applicatieRepository;
            //https://app.mailgun.com/
            //lucasvermeulen@gmail.com - 4951927700
            this.smtpClient = new SmtpClient("smtp.mailgun.org", 587);
            this.smtpClient.Credentials = new System.Net.NetworkCredential("postmaster@sandbox527ab39af7e14efe87d1c4fa0defd693.mailgun.org", "9855808fce47a7a2116caadb321cd308-7fba8a4e-dd172dfa");
            this.smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.smtpClient.EnableSsl = true;
        }

        [HttpGet("{id}")]
        public ActionResult<Applicatie> GetItemById(string id)
        {
            Applicatie a = _applicatieRepository.GetBy(id);
            if (a == null) return NotFound("Het applicaite met opgegeven id kon niet worden gevonden.");
            var folderPad = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), a.Email);
            if (Directory.Exists(folderPad))
            {
                DirectoryInfo d = new DirectoryInfo(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), a.Email));
                List<String> namen = d.GetFiles().Select(t => t.Name.ToString()).OrderBy(t => t).ToList();
                a.attestNaam = namen[0];
                a.diplomaNaam = namen[1];
                a.ReispaspoortNaam = namen[2];
            }
            return a;
        }

        [HttpGet("getByEmailEnAchternaam/{email}/{achternaam}")]
        public ActionResult<Applicatie> GetByEmailEnAchternaam(string email, string achternaam)
        {
            Applicatie a = _applicatieRepository.GetByEmailAchternaam(email, achternaam);
            if (a == null) return NotFound("Het applicaite met opgegeven email en achternaam  kon niet worden gevonden.");
            DirectoryInfo d = new DirectoryInfo(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), a.Email));
            FileInfo[] Files = null;
            List<String> namen = d.GetFiles().Select(t => t.Name.ToString()).OrderBy(t => t).ToList();
            a.attestNaam = namen[0];
            a.diplomaNaam = namen[1];
            a.ReispaspoortNaam = namen[2];
            return a;
        }

        [HttpDelete("{id}")]
        public ActionResult<Applicatie> VerwijderApplicatie(string id)
        {
            Applicatie g = _applicatieRepository.GetBy(id);
            if (g == null) return NotFound("De applicatie met opgegeven id kon niet worden gevonden.");
            var folderPad = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), g.Email);
            DirectoryInfo d = new DirectoryInfo(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), g.Email));
            FileInfo[] Files = null;
            if (Directory.Exists(folderPad))
            {
                Files = d.GetFiles("");
                foreach (FileInfo file in Files)
                {
                    var deletePath = Path.Combine(folderPad, file.Name);
                    if (System.IO.File.Exists(deletePath))
                    {
                        System.IO.File.Delete(deletePath);
                    }
                }
            }
            _applicatieRepository.Delete(g);
            _applicatieRepository.SaveChanges();
            return g;
        }

        [HttpGet("getAll")]
        public List<Applicatie> GetApplicaties()
        {
            return _applicatieRepository.GetAll();
        }

        [HttpPost]
        public ActionResult<Applicatie> VoegItemToe(ApplicatieDTO applicatieDTO)
        {
            if (_applicatieRepository.GetByEmail(applicatieDTO.Email) != null)
            {
                return BadRequest("Er betaat al een applicatie met dit email adres!");
            }
            Applicatie a = new Applicatie(applicatieDTO);
            _applicatieRepository.Add(a);
            _applicatieRepository.SaveChanges();
            return a;
        }

        [HttpPut("{id}")]
        public ActionResult<Applicatie> UpdateApp(ApplicatieDTO applicatieDTO, string id)
        {
            if (applicatieDTO.Id != id) return BadRequest("Er liep iets fout: de id's komen niet overeen.");
            Applicatie a = _applicatieRepository.GetBy(id);
            if(a == null) return BadRequest("Er kon geen applicatie met opgegeven id worden gevonden!");
            a.UpdateDezeApplicatie(applicatieDTO);
            _applicatieRepository.Update(a);
            _applicatieRepository.SaveChanges();
            if(a.HuidigeStap == 6)
            {
                try
                {
                    this.smtpClient.Send("lucas@ananau.org", "info@ananau.org", "Nieuwe Applicatie",
                        "Beste,\n" +
                        "Er werd net een nieuwe applicatie ingevuld door " + a.Voornaam + " " + a.Achternaam + " \n" +
                        "De applicatie kan bekeken worden op http://localhost:4200/applicatie-bekijken/" + a.Id + "\n"
                        + "Met vriendelijke groet, \n" +
                        "Webmaster Lucas!");
                }catch (Exception e)
                {
                    return BadRequest("De applicatie kon niet worden ingediend! Probeer het later opnieuw!");
                }
            }
            return a;
        }
    }
}
