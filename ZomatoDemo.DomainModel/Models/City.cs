using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ZomatoDemo.DomainModel.Models
{
    public class City
    {
        public int ID { get; set; }
        public string CityName { get; set; }

    }
}
