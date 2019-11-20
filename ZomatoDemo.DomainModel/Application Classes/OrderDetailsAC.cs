using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class OrderDetailsAC
    {
        public int RestaurantID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public ICollection<DishesOrdered> DishesOrdered { get; set; }
    }
}
