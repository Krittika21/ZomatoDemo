using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.DomainModel.Application_Classes;

namespace ZomatoDemo.DomainModel.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int LikesCount { get; set; }
        public string ReviewTexts { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual UserAC User { get; set; }
    }
}
