using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZomatoDemo.DomainModel.Models
{
    public class User : IdentityUser
    {
        public string Id { get; set; }
        public string FullName { get; set; }
    }
}
