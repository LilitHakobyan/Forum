using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Entity
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ThreadId { get; set; }
        public  int UserId { get; set; }
}
}