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
        public DateTime CreatedAt { get; set; } 
        public List<Post> ThreadPosts { get; set; }
        public Guid UserId { get; set; }
        public int TopicId { get; set; }

        public Thread()
        {
            ThreadPosts = new List<Post>();
        }
    }
}