using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Models
{
    public class Rating
    {
        public int ID { get; set; }
        public decimal AverageRating { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual User User { get; set; }
    }
}
