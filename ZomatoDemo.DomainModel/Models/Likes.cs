using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.DomainModel.Application_Classes;

namespace ZomatoDemo.DomainModel.Models
{
    public class Likes
    {
        public int ID { get; set; }

        public virtual Review Reviews { get; set; }
        public virtual UserAC Users { get; set; }
    }
}
