using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Login not specified")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Name not specified")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Email not specified")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords doesn't match")]
        public string Confirm { get; set; }
        public string Phone { get; set; }
    }
}
