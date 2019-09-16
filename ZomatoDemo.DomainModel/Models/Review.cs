using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int LikesCount { get; set; }
        public string ReviewTexts { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual User User { get; set; }
    }
}
