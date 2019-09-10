using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Models
{
    public class Restaurants
    {
        public int ID { get; set; }
        public string RestaurantName { get; set; }

        public virtual ICollection<Location> Location { get; set; }
        public virtual ICollection<Dishes> Dishes { get; set; }
    }
}
