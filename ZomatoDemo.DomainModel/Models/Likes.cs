using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Models
{
    public class Likes
    {
        public int ID { get; set; }

        public virtual Review Reviews { get; set; }
        public virtual User Users { get; set; }
    }
}
