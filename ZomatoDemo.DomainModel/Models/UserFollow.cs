using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZomatoDemo.DomainModel.Application_Classes;

namespace ZomatoDemo.DomainModel.Models
{
    public class UserFollow
    {
        public int ID { get; set; }

        public string FolloweeId { get; set; }
        public string FollowerId { get; set; }

        [ForeignKey("FolloweeId")]
        public virtual User Followee { get; set; }

        [ForeignKey("FollowerId")]
        public virtual User Follower { get; set; }
    }
}
