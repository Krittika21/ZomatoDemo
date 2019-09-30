using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.DomainModel.Application_Classes;

namespace ZomatoDemo.DomainModel.Models
{
    public class Rating
    {
        public int ID { get; set; }
        public decimal AverageRating { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual UserAC User { get; set; }
    }
}
