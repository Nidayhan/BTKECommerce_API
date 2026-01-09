using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.DTOs.User
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Passwords Does Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
