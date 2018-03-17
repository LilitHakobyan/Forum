using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Entity
{
    public class Thread
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TextDescription { get; set; }
        public List<Post> ThreadPosts { get; set; }
        public int UserId { get; set; }
    }
}