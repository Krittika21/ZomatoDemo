using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class AllDetails
    {
        //public int ID { get; set; }
        public int LocationID { get; set; }
        public int RestaurantID { get; set; }
        //public string Locality { get; set; }
        public ICollection<Location> Locations { get; set; }
        public string RestaurantName { get; set; } 
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string CuisineType { get; set; }
        public string AverageCost { get; set; }
        public string OpeningHours { get; set; }
        public string MoreInfo { get; set; }

        public AllCity City { get; set; }
        public AllCountry Country { get; set; }
        public List<CommentAC> Comments { get; set; }
        public List<ReviewsAC> Reviews { get; set; }

    }
}
