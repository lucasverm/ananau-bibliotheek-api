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
    public class GebruikerController : ControllerBase
    {
        private readonly SignInManager<Gebruiker> _signInManager;
        private readonly UserManager<Gebruiker> _userManager;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IConfiguration _config;
        public GebruikerController(SignInManager<Gebruiker> signInManager, UserManager<Gebruiker> userManager,
            IGebruikerRepository gebruikerRepository, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _gebruikerRepository = gebruikerRepository;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateToken(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    string token = GetToken(user);
                    return Created("", new { token, user }); //returns only the token                   
                }
            }
            return BadRequest();
        }

        private string GetToken(Gebruiker g)
        {      // Createthetoken
         
         
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(null, null,
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO model)
        {
            Gebruiker g = new Gebruiker
            {
                Email = model.Email,
                Voornaam = model.Voornaam,
                Achternaam = model.Achternaam,
                Foto = model.Foto,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(g, model.Password);
            if (result.Succeeded)
            {
                _gebruikerRepository.SaveChanges();
                string token = GetToken(g);

                var host = Request.Host;
                return Created($"https://{host}/api/account/{g.Id}", g);
            }
            return BadRequest();
        }

        [HttpGet("{gebruikerId}")]
        public ActionResult<GebruikerDTO> GetGebruikerId(string gebruikerId)
        {
            Gebruiker g = _gebruikerRepository.GetBy(gebruikerId);
            if (g == null) return NotFound();
            return new GebruikerDTO(g);
        }

        [HttpDelete("{id}")]
        public ActionResult<Gebruiker> VerwijderGebruiker(string id)
        {
            Gebruiker g = _gebruikerRepository.GetBy(id);
            if (g == null)
            {
                return NotFound();
            }
            _gebruikerRepository.Delete(g);
            _gebruikerRepository.SaveChanges();
            return g;
        }

        [HttpGet]
        public IEnumerable<GebruikerDTO> GetGebruikers()
        {
            return _gebruikerRepository.GetAll().Select(g => new GebruikerDTO(g));
        }

        [HttpPut("{id}")]
        public ActionResult<Gebruiker> PutGebruiker(string id, GebruikerDTO gebruiker)
        {
            if (!gebruiker.Id.Equals(id))
                return BadRequest();

            Gebruiker g = _gebruikerRepository.GetBy(id);

            g.Achternaam = gebruiker.Achternaam;
            g.Voornaam = gebruiker.Voornaam;
            g.Email = gebruiker.Email;
            g.Foto = gebruiker.Foto;
       
            _gebruikerRepository.Update(g);
            _gebruikerRepository.SaveChanges();
            return NoContent();
        }
    }
}
