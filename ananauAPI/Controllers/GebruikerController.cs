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
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return user == null;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateToken(LoginDTO model)
        {
            var user = _gebruikerRepository.GetByEmail(model.Email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                var claims = await _signInManager.UserManager.GetClaimsAsync(user);
                if (result.Succeeded)
                {
                    string token = GetToken(user, claims);
                    return Created("", new { token, user }); //returns only the token                   
                }
                else
                {
                    return BadRequest("Dit wachtwoord is onjuist.");
                }
            }
            else
            {
                return BadRequest("Dit ema" +
                    "il adres is onjuist.");
            }
        }

        private string GetToken(Gebruiker g, IList<Claim> claims)
        {      // Createthetoken
            var claimarray = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, g.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, g.UserName)};

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, g.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, g.UserName));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(null, null,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO model)
        {
            Gebruiker user = new Gebruiker
            {
                Email = model.Email,
                Voornaam = model.Voornaam,
                Achternaam = model.Achternaam,
                UserName = model.Email,
                TelefoonNummer = model.TelefoonNummer,
                GeboorteDatum = model.GeboorteDatum
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
            var claims = await _signInManager.UserManager.GetClaimsAsync(user);
            if (result.Succeeded)
            {
                _gebruikerRepository.SaveChanges();

                string token = GetToken(user, claims);
                return Created("", new { token, user });
            }
            return BadRequest();
        }

        [HttpGet("{gebruikerId}")]
        public ActionResult<Gebruiker> GetGebruikerId(string gebruikerId)
        {
            Gebruiker g = _gebruikerRepository.GetBy(gebruikerId);
            if (g == null) return NotFound();
            return g;
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
        public IEnumerable<Gebruiker> GetGebruikers()
        {
            return _gebruikerRepository.GetAll();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Gebruiker>> PutGebruikerAsync(string id, UpdateGebruikerDTO gebruiker)
        {
            if (!gebruiker.Id.Equals(id))
                return BadRequest();
            Gebruiker g = _gebruikerRepository.GetBy(id);
            g.Achternaam = gebruiker.Achternaam;
            g.Voornaam = gebruiker.Voornaam;
            var user = await _userManager.FindByEmailAsync(g.Email);
            user.UserName = gebruiker.Email;
            user.Email = gebruiker.Email;
            await _userManager.UpdateAsync(user);
            g.Email = gebruiker.Email;
            g.TelefoonNummer = gebruiker.TelefoonNummer;
            g.GeboorteDatum = gebruiker.GeboorteDatum;
            _gebruikerRepository.Update(g);
            _gebruikerRepository.SaveChanges();
            return g;
        }
    }
}
