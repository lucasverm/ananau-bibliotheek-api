using System;
using System.ComponentModel.DataAnnotations;

namespace ananauAPI.DTO
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        [StringLength(200)]
        public string Voornaam { get; set; }
        [Required]
        [StringLength(250)]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Please enter your password again")]
        [Compare("Password", ErrorMessage = "Password and passwordconfirmation must be the same")]
        public string PasswordConfirmation { get; set; }

        public string Foto { get; set; }
    }
}
