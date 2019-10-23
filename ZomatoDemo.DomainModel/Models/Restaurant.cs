using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string RestaurantName { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string CuisineType { get; set; }
        public string AverageCost { get; set; }
        public string OpeningHours { get; set; }
        public string MoreInfo { get; set; }

        public virtual ICollection<Location> Location { get; set; }
        public virtual ICollection<Dishes> Dishes { get; set; }
    }
}
