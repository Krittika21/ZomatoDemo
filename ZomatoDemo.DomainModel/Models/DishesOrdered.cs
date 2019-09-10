using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Models
{
    public class DishesOrdered
    {
        public int ID { get; set; }
        public int ItemsCount { get; set; }

        public virtual Dishes Dishes { get; set; }
        public virtual OrderDetails Order { get; set; }
    }
}
