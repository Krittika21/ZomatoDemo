using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.DomainModel.Application_Classes
{
    public class ReviewsAC
    {
        public string userID { get; set; }
        public string UserName { get; set; }
        public int LikesCount { get; set; }
        public int ReviewId { get; set; }
        public string ReviewTexts { get; set; }
        public List<CommentAC> commentACs { get; set; }
    }
}
