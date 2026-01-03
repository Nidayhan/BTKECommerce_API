using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BTKECommerce_core.DTOs.User
{
    public class RegisterDTO
    {

        public string Email { get; set; } 
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
