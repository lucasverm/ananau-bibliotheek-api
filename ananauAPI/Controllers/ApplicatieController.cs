using System;
using System.Collections.Generic;
using System.Linq;
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
        public ApplicatieController(IApplicatieRepository applicatieRepository)
        {
            _applicatieRepository = applicatieRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Applicatie> GetItemById(string id)
        {
            Applicatie a = _applicatieRepository.GetBy(id);
            if (a == null) return NotFound("Het applicaite met opgegeven id kon niet worden gevonden.");
            return a;
        }

        [HttpGet("getByEmailEnAchternaam/{email}/{achternaam}")]
        public ActionResult<Applicatie> GetByEmailEnAchternaam(string email, string achternaam)
        {
            Applicatie a = _applicatieRepository.GetByEmailAchternaam(email, achternaam);
            if (a == null) return NotFound("Het applicaite met opgegeven email en achternaam  kon niet worden gevonden.");
            return a;
        }

        [HttpDelete("{id}")]
        public ActionResult<Applicatie> VerwijderApplicatie(string id)
        {
            Applicatie g = _applicatieRepository.GetBy(id);
            if (g == null) return NotFound("De applicatie met opgegeven id kon niet worden gevonden.");
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
            return a;
        }
    }
}
