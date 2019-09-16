using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Models
{
    public class UserFollow
    {
        public int ID { get; set; }

        public virtual User Followee { get; set; }
        public virtual User Follower { get; set; }
    }
}
