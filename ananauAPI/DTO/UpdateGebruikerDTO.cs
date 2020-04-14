using System;
using System.ComponentModel.DataAnnotations;

namespace ananauAPI.DTO
{
    public class UpdateGebruikerDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Please provide an emailaddress")]
        [EmailAddress]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Please provide a valid emailaddress")]
        public string Email { get; set; }
        [Required]
        [StringLength(200)]
        public string Voornaam { get; set; }
        [Required]
        [StringLength(250)]
        public string Achternaam { get; set; }
        public string TelefoonNummer { get; set; }
        [Required(ErrorMessage = "GeboorteDatum is verplicht!")]
        public DateTime GeboorteDatum { get; set; }
    }
}
