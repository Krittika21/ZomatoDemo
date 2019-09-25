using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class OrderDetailsAC
    {
        public int RestaurantID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public List<int> DishesID { get; set; }
    }
}
