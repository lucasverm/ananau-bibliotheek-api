using System;
using System.ComponentModel.DataAnnotations;

namespace ananauAPI.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Please provide an emailaddress")]
        [EmailAddress]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Please provide a valid emailaddress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide a password")]
        public string Password { get; set; }
    }
}
