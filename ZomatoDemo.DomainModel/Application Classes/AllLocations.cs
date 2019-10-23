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

        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string CuisineType { get; set; }
        public string AverageCost { get; set; }
        public string OpeningHours { get; set; }
        public string MoreInfo { get; set; }

        public AllCity City { get; set; }
        public AllCountry Country { get; set; }
    }
}
