using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZomatoDemo.DomainModel.Application_Classes;

namespace ZomatoDemo.DomainModel.Models
{
    public class Comment
    {
        public int ID{ get; set; }
        public string CommentMessage { get; set; }
        public string UserID { get; set; }
        public int ReviewID { get; set; }

        [ForeignKey("ReviewID")]
        public virtual Review Review { get; set; }
        [ForeignKey("UserID")]
        public virtual UserAC User { get; set; }
    }
}
