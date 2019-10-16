using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class AllLocations
    {
        public int ID { get; set; }
        public string Locality { get; set; }

        public List<string> City { get; set; }
        public List<string> Country { get; set; }
    }
}
