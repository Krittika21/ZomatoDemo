using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class OrderDetailsAC
    {
        public List<int> RestaurantID { get; set; }
        public List<string> UserName { get; set; }
        public List<string> DishesName { get; set; }
    }
}
