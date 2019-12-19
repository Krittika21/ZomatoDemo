using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.DomainModel.Application_Classes;

namespace ZomatoDemo.DomainModel.Models
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<DishesOrdered> DishesOrdered { get; set; }
    }
}
