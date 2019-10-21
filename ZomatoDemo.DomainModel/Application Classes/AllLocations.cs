using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class AllLocations
    {
        public int ID { get; set; }
        public string Locality { get; set; }
        public string RestaurantName { get; set; }

        public AllCity City { get; set; }
        public AllCountry Country { get; set; }
    }
}
