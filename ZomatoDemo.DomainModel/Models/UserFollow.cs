using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Models
{
    public class UserFollow
    {
        public int ID { get; set; }

        public virtual Users Followee { get; set; }
        public virtual Users Follower { get; set; }
    }
}
