using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class RestaurantAC
    {
        public List<string> DishesName { get; set; }
        public string RestaurantName { get; set; }
        public List<int> LocationID { get; set; }
    }
}
