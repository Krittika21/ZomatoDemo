using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class UserAC : IdentityUser
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
