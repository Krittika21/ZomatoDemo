using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class CurrentUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<string> Roles { get; set; }
        public string email { get; set; }
    }
}
