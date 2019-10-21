using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZomatoDemo.DomainModel.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string Locality { get; set; }

        public int CityID { get; set; }
        public int CountryID { get; set; }

        [ForeignKey("CityID")]
        public virtual City City { get; set; }
        [ForeignKey("CountryID")]
        public virtual Country Country { get; set; }
    }
}
