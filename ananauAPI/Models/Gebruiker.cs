using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

namespace ananauAPI.Models
{
    public class Gebruiker : IdentityUser
    {

        private string _voornaam;
        private string _achternaam;
        private string _email;
        private string _foto;
        override public string Id { get; set; }
        public IdentityUserClaim<string> Role { get; set; }
        public string Voornaam
        {
            get
            {
                return _voornaam;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een gebruiker moet een voornaam hebben");
                _voornaam = value;
            }
        }

        public string Achternaam
        {
            get
            {
                return _achternaam;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een gebruiker moet een achternaam hebben");
                _achternaam = value;
            }
        }


        public override string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een gebruiker moet een email hebben");
                else if (!Regex.IsMatch(value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                    throw new ArgumentException("Ongeldig email formaat");
                _email = value;
            }
        }

        public string Foto
        {
            get
            {
                return _foto;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een gebruiker moet een foto hebben");
                _foto = value;
            }
        }
        public List<GebruikerItem> GebruikerItems { get; set; }

        public Gebruiker()
        {
            GebruikerItems = new List<GebruikerItem>();
        }

        public Gebruiker(string voornaam, string achternaam, string email, string foto) : this()
        {
            Voornaam = voornaam;
            Achternaam = achternaam;
            Email = email;
            Foto = foto;
            UserName = email;
        }
    }
}
