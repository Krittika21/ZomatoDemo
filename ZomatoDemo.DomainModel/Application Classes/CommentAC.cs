using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class CommentAC
    {
        public int ID { get; set; }
        public string CommentMessage { get; set; }
        public string UserID { get; set; }
        public string FullName { get; set; }
        public int ReviewID { get; set; }
    }
}
