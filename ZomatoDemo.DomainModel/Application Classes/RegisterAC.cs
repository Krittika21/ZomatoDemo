using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class RegisterAC
    {
        //[Required]
        //[MaxLength(256)]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
